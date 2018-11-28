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
    [RoutePrefix("api/Presets")]
    public class PresetsController : ApiController
    {
        private DAMContext db = new DAMContext();

        [HttpGet, Route("{token}/GetPresetss")]
        [ResponseType(typeof(IQueryable<Presets>))]
        // GET: api/Presets
        public IHttpActionResult GetPresetss(string token)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");
            return Ok(db.Presetss);
        }

        // GET: api/Presets/5
        [ResponseType(typeof(Presets))]
        public IHttpActionResult GetPresets(int id)
        {
            Presets presets = db.Presetss.Find(id);
            if (presets == null)
            {
                return NotFound();
            }

            return Ok(presets);
        }

        // PUT: api/Presets/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPresets(int id, Presets presets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != presets.ID)
            {
                return BadRequest();
            }

            db.Entry(presets).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PresetsExists(id))
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

        // POST: api/Presets
        [HttpPost, Route("{token}/PostPresets")]
        [ResponseType(typeof(Presets))]
        public IHttpActionResult PostPresets(string token, Presets presets)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Presetss.Add(presets);
            db.SaveChanges();
            db.Entry(presets).GetDatabaseValues();

            return Ok(presets);
        }

        // DELETE: api/Presets/5
        [HttpDelete, Route("{token}/DeletePresets/{id}")]
        [ResponseType(typeof(Presets))]
        public IHttpActionResult DeletePresets(string token, int id)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");
            Presets presets = db.Presetss.Find(id);
            if (presets == null)
            {
                return NotFound();
            }

            db.Presetss.Remove(presets);
            db.SaveChanges();

            return Ok(presets);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PresetsExists(int id)
        {
            return db.Presetss.Count(e => e.ID == id) > 0;
        }
    }
}