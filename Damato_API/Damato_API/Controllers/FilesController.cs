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

        // PUT: api/Files/5
        [HttpPost, Route("{token}/UploadFile")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UploadFile(string token, TFile file)
        {
            Token _token = db.Tokens.Include(t => t.User).Single(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            System.IO.File.WriteAllBytes($@"C:\Users\sbnbl\Desktop\New folder\{file.Path}", file.File);
            

            Damato_API.DataBase.File file2 = new Damato_API.DataBase.File()
            {
                Path = $@"C:\Users\sbnbl\Desktop\New folder\{file.Path}",
                Level = $"{_token.User.Level},{_token.User.Level},{_token.User.Level}"
            };
            db.Files.Add(file2);
            db.SaveChanges();
            db.Entry(file2).Reload();
            var user = db.Users.ToList().SingleOrDefault(u => u.ID == _token.User.ID);
            file2.User = user;
            db.SaveChanges();
            
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