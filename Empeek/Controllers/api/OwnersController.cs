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
using Empeek.Models;
namespace Empeek.Controllers.api
{
    [RoutePrefix("api/Owners")]
    public class OwnersController : ApiController
    {
        private OnPContext db = new OnPContext();

        // GET(all)
        [HttpGet]
        public IQueryable<Owner> AllOwners()
        {
            return db.Owners;
        }

        // GET(ByID)
        [Route("{ownerid:int}")]
        [HttpGet]
        [ResponseType(typeof(Owner))]
        public IHttpActionResult OwnerById(int ownerid)
        {
            Owner owner = db.Owners.Find(ownerid);
            if (owner == null)
            {
                return NotFound();
            }

            return Ok(owner);
        }



        // POST
        [HttpPost]
        [ResponseType(typeof(Owner))]
        public IHttpActionResult AddOwner(Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Owners.Add(owner);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = owner.OwnerId }, owner);
        }

        // DELETE
        [Route("{ownerid:int}")]
        [HttpDelete]
        [ResponseType(typeof(Owner))]
        public IHttpActionResult DeleteOwner(int ownerid)
        {
            Owner owner = db.Owners.Find(ownerid);
            if (owner == null)
            {
                return NotFound();
            }
            db.Pets.Load();
            db.Owners.Remove(owner);
            db.SaveChanges();

            return Ok(owner);
        }

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