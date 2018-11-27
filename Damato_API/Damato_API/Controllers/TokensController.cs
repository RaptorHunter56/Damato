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
    public class TokensController : ApiController
    {
        public Token NewToken(User user)
        {
            user = db.Users.ToList().SingleOrDefault(u => u.Name == user.Name && u.PasswordDecrypted == user.PasswordDecrypted);
            if (user == null)
                return new Token() { _Token = "NotFound" };
            Token token;
            do
            {
                token = new Token()
                {
                    _Token = (DateTime.Now.ToString("fffffffK") + "0000000000").Substring(0, 10)
                };
            } while (TokenExists(token._Token));
            db.Tokens.Add(token);
            db.SaveChanges();
            db.Entry(token).Reload();
            token.User = user;
            db.SaveChanges();
            return token;
        }

        private DAMContext db = new DAMContext();

        // GET: api/Tokens
        public IQueryable<Token> GetTokens()
        {
            return db.Tokens;
        }

        // GET: api/Tokens/5
        [ResponseType(typeof(Token))]
        public IHttpActionResult GetToken(string id)
        {
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                return NotFound();
            }

            return Ok(token);
        }

        // PUT: api/Tokens/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutToken(string id, Token token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != token._Token)
            {
                return BadRequest();
            }

            db.Entry(token).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TokenExists(id))
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

        // POST: api/Tokens
        [ResponseType(typeof(Token))]
        public IHttpActionResult PostToken(Token token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tokens.Add(token);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TokenExists(token._Token))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = token._Token }, token);
        }

        // DELETE: api/Tokens/5
        [ResponseType(typeof(Token))]
        public IHttpActionResult DeleteToken(string id)
        {
            Token token = db.Tokens.Find(id);
            if (token == null)
            {
                return NotFound();
            }

            db.Tokens.Remove(token);
            db.SaveChanges();

            return Ok(token);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TokenExists(string id)
        {
            return db.Tokens.Count(e => e._Token == id) > 0;
        }
    }
}