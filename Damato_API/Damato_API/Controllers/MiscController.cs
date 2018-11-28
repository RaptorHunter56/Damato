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
    [RoutePrefix("api/Misc")]
    public class MiscController : ApiController
    {
        [HttpGet, Route("{token}/GetAllFilesTypes")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetAllFilesTypes(string token)
        {
            Token _token = db.Tokens.Include(t => t.User).Single(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            var result = db.Files.Where(f => f.RLevel >= _token.User.Level).Select(f => f.Path);
            List<string> vs = new List<string>();
            foreach (var item in result)
            { vs.Add($".{item.Split('.').Last()}"); }
            return Ok(vs.Distinct().ToList());
        }

        [HttpGet, Route("{token}/GetAllFilesTags")]
        [ResponseType(typeof(List<string>))]
        public IHttpActionResult GetAllFilesTags(string token)
        {
            Token _token = db.Tokens.Include(t => t.User).FirstOrDefault(t => t._Token == token);
            if (_token == null)
                return Content(HttpStatusCode.Unauthorized, "Token Does Not Exist");
            if (_token.DateExpiered.CompareTo(DateTime.Now) < 0)
                return Content(HttpStatusCode.Unauthorized, "Token Expired");

            var result1 = db.Files.Include(f => f.MainTags);
            List<File> resultf = new List<File>();
            foreach (var item in result1)
            {
                try
                {  if (item.RLevel >= _token.User.Level) resultf.Add(item); }
                catch { }
            }
            List<Tag> result = new List<Tag>();
            foreach (var item in resultf)
            {
                try
                { result.AddRange(item.MainTags); }
                catch { }
            }
            List<string> vs = new List<string>();
            foreach (var item in result)
            { vs.Add($"{item._Tag}"); }
            return Ok(vs.Distinct().ToList());
        }

        private DAMContext db = new DAMContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
