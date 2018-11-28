using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Damato_API.DataBase;
using Damato_API.Settings;
using Newtonsoft.Json;

namespace Damato_API.Controllers
{
    [RoutePrefix("api/Files")]
    public class FilesController : ApiController
    {
        public static string PathLocation = @"D:\home\site\wwwroot\Damato_API";

        // GET: api/Files/2460348+13/GetRecentFiles
        [HttpGet, Route("{token}/GetRecentFiles")]
        [ResponseType(typeof(List<File>))]
        public IHttpActionResult GetRecentFiles(string token, int amount = 10)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            IEnumerable<File> files = db.Files.Include(f => f.User).Include(f => f.MainTags).OrderBy(f => f.DateAdded);
            files = files.Reverse().Take(amount);
            foreach (var item in files)
            {
                item.User.Password = "*****";
            }
            if (files == null)
                return NotFound();
            return Ok(files.Where(f => f.RLevel >= _token.User.Level));
        }

        // PUT: api/Files/2460348+13/DownloadFile
        [HttpPost, Route("{token}/DownloadFile")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DownloadFile(string token, string filename)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            File _file = db.Files.Where(f => f.Path.Contains(filename) && f.WLevel >= _token.User.Level).FirstOrDefault();
            if (_file == null)
                return NotFound();
            OutSettings o;
            try
            {
                string json1 = System.IO.File.ReadAllText($@"{PathLocation}\ApplicationSettings.json");
                o = JsonConvert.DeserializeObject<OutSettings>(json1);
            }
            catch (Exception)
            {
                o = new OutSettings();
            }
            o.FileOut.Add(_file.PathParts.Last(), _token.User.ID);
            string json = JsonConvert.SerializeObject(o);
            System.IO.File.WriteAllText($@"{PathLocation}\ApplicationSettings.json", json);
            byte[] temp = System.IO.File.ReadAllBytes(_file.Path);
            return Ok(Convert.ToBase64String(temp));

        }

        // PUT: api/Files/2460348+13/UploadFile
        [HttpPost, Route("{token}/UploadFile/{returnfile}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UploadFile(string token, TFile file, string returnfile = "false")
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            if (returnfile == "true")
            {
                if (db.Files.Where(f => f.Path == $@"{PathLocation}\{file.Path}" && f.WLevel >= _token.User.Level).FirstOrDefault() == null)
                    return Content(HttpStatusCode.Unauthorized, "File Does Not Exist");
            }
            else
            {
                int coppy = 1;
                while (System.IO.File.Exists($@"{PathLocation}\{file.Path}"))
                {
                    try
                    {
                        file.Path = file.Path.Substring(0, file.Path.Length - file.Path.Split('_').Last().Length - 1) + $"_{coppy}." + file.Path.Split('.').Last();
                    }
                    catch
                    {
                        file.Path = file.Path.Substring(0, file.Path.Length - file.Path.Split('.').Last().Length - 1) + $"_{coppy}." + file.Path.Split('.').Last();
                    }
                    coppy++;
                }
            }

            System.IO.File.WriteAllBytes($@"{PathLocation}\{file.Path}", file.File);
            
            Damato_API.DataBase.File file2 = new Damato_API.DataBase.File()
            {
                Path = $@"{PathLocation}\{file.Path}",
                Level = $"{_token.User.Level},{_token.User.Level},{_token.User.Level}"
            };
            
            if (returnfile == "true")
            {
                string json = System.IO.File.ReadAllText($@"{PathLocation}\ApplicationSettings.json");
                OutSettings o = JsonConvert.DeserializeObject<OutSettings>(json);
                o.FileOut.Remove(file2.PathParts.Last());
                json = JsonConvert.SerializeObject(o);
                System.IO.File.WriteAllText($@"{PathLocation}\ApplicationSettings.json", json);
            }
            else
            {
                db.Files.Add(file2);
                db.SaveChanges();
                db.Entry(file2).Reload();
                var user = db.Users.ToList().FirstOrDefault(u => u.ID == _token.User.ID);
                file2.User = user;
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // PUT: api/Files/2460348+13/UploadFileTaged
        [HttpPost, Route("{token}/UploadFileTaged/{returnfile}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UploadFileTaged(string token, CFile file, string returnfile = "false")
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            if (returnfile == "true")
            {
                if (db.Files.Where(f => f.Path == $@"{PathLocation}\{file.Path}" && f.WLevel >= _token.User.Level).FirstOrDefault() == null)
                    return Content(HttpStatusCode.Unauthorized, "File Does Not Exist");
            }
            else
            {
                int coppy = 1;
                while (System.IO.File.Exists($@"{PathLocation}\{file.Path}"))
                {
                    try
                    {
                        file.Path = file.Path.Substring(0, file.Path.Length - file.Path.Split('_').Last().Length - 1) + $"_{coppy}." + file.Path.Split('.').Last();
                    }
                    catch
                    {
                        file.Path = file.Path.Substring(0, file.Path.Length - file.Path.Split('.').Last().Length - 1) + $"_{coppy}." + file.Path.Split('.').Last();
                    }
                    coppy++;
                }
            }

            System.IO.File.WriteAllBytes($@"{PathLocation}\{file.Path}", file.File);

            Damato_API.DataBase.File file2 = new Damato_API.DataBase.File()
            {
                Path = $@"{PathLocation}\{file.Path}",
                Level = $"{_token.User.Level},{_token.User.Level},{_token.User.Level}"
            };

            if (returnfile == "true")
            {
                string json = System.IO.File.ReadAllText($@"{PathLocation}\ApplicationSettings.json");
                OutSettings o = JsonConvert.DeserializeObject<OutSettings>(json);
                o.FileOut.Remove(file2.PathParts.Last());
                json = JsonConvert.SerializeObject(o);
                System.IO.File.WriteAllText($@"{PathLocation}\ApplicationSettings.json", json);
            }
            else
            {
                db.Files.Add(file2);
                db.SaveChanges();
                db.Entry(file2).Reload();
                foreach (var item in file.Tags)
                {
                    db.Entry(file2).Reload();
                    Damato_API.DataBase.Tag s = new Tag()
                    {
                        _Tag = item,
                        File_ID = file2
                    };
                    db.Tags.Add(s);
                    db.SaveChanges();
                    db.Entry(s).Reload();
                    db.Entry(file2).Reload();
                }
                db.Entry(file2).Reload();
                var user = db.Users.ToList().FirstOrDefault(u => u.ID == _token.User.ID);
                file2.User = user;
                db.SaveChanges();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // GET: api/Files/2460348+13/GetRecentFiles
        [HttpGet, Route("{token}/SearchRecentFiles")]
        [ResponseType(typeof(List<File>))]
        public IHttpActionResult SearchRecentFiles(string token, [FromUri] string[] search, int amount = 10)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            IEnumerable<File> files = db.Files.Include(f => f.User).Include(f => f.MainTags).OrderBy(f => f.DateAdded);
            List<File> temp = new List<File>();
            foreach (var item in search.Where(s => s[0] == '.'))
            {
                temp.AddRange(files.Where(f => ("." + f.Path.Split('.').Last()) == item));
            }
            if (temp.Count() > 0)
                files = temp;
            foreach (var item in search.Where(s => s[0] == '*'))
            {
                files = files.Where(f => (f.MainTags.Where(g => g._Tag == item.Substring(1)).Count() > 0));
            }
            foreach (var item in search.Where(s => s[0] != '.'))
            {
                files = files.Where(f => (f.Path.Split('\\').Last().Contains(item)));
            }
            files = files.Reverse().Take(amount);
            foreach (var item in files)
            {
                item.User.Password = "*****";
            }
            if (files == null)
                return NotFound();
            return Ok(files.Where(f => f.RLevel >= _token.User.Level));
        }

        private DAMContext db = new DAMContext();

        // GET: api/Files
        public IQueryable<File> GetFiles()
        {
            return db.Files;
        }

        // GET: api/Files/5
        [ResponseType(typeof(File))]
        public IHttpActionResult GetFile(int id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        // PUT: api/Files/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFile(int id, File file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != file.ID)
            {
                return BadRequest();
            }

            db.Entry(file).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Files
        [ResponseType(typeof(File))]
        public IHttpActionResult PostFile(File file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Files.Add(file);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = file.ID }, file);
        }

        // DELETE: api/Files/5
        [ResponseType(typeof(File))]
        public IHttpActionResult DeleteFile(int id)
        {
            File file = db.Files.Find(id);
            if (file == null)
            {
                return NotFound();
            }

            db.Files.Remove(file);
            db.SaveChanges();

            return Ok(file);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FileExists(int id)
        {
            return db.Files.Count(e => e.ID == id) > 0;
        }
    }
}