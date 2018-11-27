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
        // GET: api/Files/2460348+13/GetRecentFiles
        [HttpGet, Route("{token}/GetRecentFiles")]
        [ResponseType(typeof(List<File>))]
        public IHttpActionResult GetRecentFiles(string token, int amount = 10)
        {
            Token _token = db.Tokens.Include(t => t.User).Single(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            IEnumerable<File> files = db.Files.Include(f => f.User).OrderBy(f => f.DateAdded);
            files = files.Where(f => f.User == _token.User).Reverse().Take(amount);
            foreach (var item in files)
            {
                item.User.Password = "*****";
            }
            if (files == null)
                return NotFound();
            return Ok(files);
        }

        // PUT: api/Files/2460348+13/DownloadFile
        [HttpPost, Route("{token}/DownloadFile")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DownloadFile(string token, string filename)
        {
            Token _token = db.Tokens.Include(t => t.User).Single(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            File _file = db.Files.Where(f => f.Path.Contains(filename)).FirstOrDefault();
            if (_file == null)
                return NotFound();
            OutSettings o;
            try
            {
                string json1 = System.IO.File.ReadAllText(@"C:\Users\Steven Bown\Desktop\New folder\ApplicationSettings.json");
                o = JsonConvert.DeserializeObject<OutSettings>(json1);
            }
            catch (Exception)
            {
                o = new OutSettings();
            }
            o.FileOut.Add(_file.PathParts.Last(), _token.User.ID);
            string json = JsonConvert.SerializeObject(o);
            System.IO.File.WriteAllText(@"C:\Users\Steven Bown\Desktop\New folder\ApplicationSettings.json", json);
            byte[] temp = System.IO.File.ReadAllBytes(_file.Path);
            return Ok(Convert.ToBase64String(temp));

        }

        // PUT: api/Files/2460348+13/UploadFile
        [HttpPost, Route("{token}/UploadFile/{returnfile}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UploadFile(string token, TFile file, string returnfile = "false")
        {
            Token _token = db.Tokens.Include(t => t.User).Single(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            System.IO.File.WriteAllBytes($@"C:\Users\Steven Bown\Desktop\New folder\{file.Path}", file.File);
            
            Damato_API.DataBase.File file2 = new Damato_API.DataBase.File()
            {
                Path = $@"C:\Users\Steven Bown\Desktop\New folder\{file.Path}",
                Level = $"{_token.User.Level},{_token.User.Level},{_token.User.Level}"
            };
            db.Files.Add(file2);
            db.SaveChanges();
            db.Entry(file2).Reload();
            var user = db.Users.ToList().SingleOrDefault(u => u.ID == _token.User.ID);
            file2.User = user;
            db.SaveChanges();
            
            if (returnfile == "true")
            {
                string json = System.IO.File.ReadAllText("ApplicationSettings.json");
                OutSettings o = JsonConvert.DeserializeObject<OutSettings>(json);
                o.FileOut.Remove(file2.PathParts.Last());
                json = JsonConvert.SerializeObject(o);
                System.IO.File.WriteAllText("ApplicationSettings.json", json);
            }

            return StatusCode(HttpStatusCode.NoContent);
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