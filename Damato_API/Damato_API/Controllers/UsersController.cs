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
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        // GET: api/Users/GetNewToken
        [HttpPost, Route("GetNewToken")]
        public string GetToken(User user)
        {
            return new TokensController().NewToken(user)._Token;
        }

        [HttpGet, Route("{token}/GetOutFiles")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetOutFiles(string token)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");
            string json = System.IO.File.ReadAllText($@"{FilesController.PathLocation}\ApplicationSettings.json");
            OutSettings Settings = JsonConvert.DeserializeObject<OutSettings>(json);
            List<string> s = new List<string>();
            foreach (var item in Settings.FileOut)
            {
                s.Add(item.Key + $"[{item.Value}]");
            }
            return Ok(s);
        }
        
        [HttpGet, Route("{token}/Getlevel")]
        [ResponseType(typeof(string))]
        public IHttpActionResult Getlevel(string token)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            return Ok(_token.User.Level.ToString());
        }

        private DAMContext db = new DAMContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.ID)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}