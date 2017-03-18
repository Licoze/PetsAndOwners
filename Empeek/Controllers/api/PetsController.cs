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
    [RoutePrefix("api/Owners/{ownerid:int}/Pets")]
    public class PetsController : ApiController
    {
        private OnPContext db = new OnPContext();

        // GET(all)
        [Route("")]
        [HttpGet]
       
        public IQueryable<Pet> AllPets(int ownerid)
        {
            db.Pets.Load();
            return db.Owners.Find(ownerid).Pets.AsQueryable();
           
        }

        //// GET(ByID)
        //[Route("{petid:int}")]
        //[HttpGet]
        //[ResponseType(typeof(Pet))]
        //public IHttpActionResult PetById(int id)
        //{
        //    Pet pet = db.Pets.Find(id);
        //    if (pet == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(pet);
        //}


        // POST
        [Route("", Name = "AddPet")]
        [HttpPost]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult AddPet(Pet pet, int ownerid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                db.Owners.Find(ownerid).Pets.Add(pet);            
                db.SaveChanges();

            return CreatedAtRoute("AddPet", new { id = pet.PetId }, pet);
        }

        // DELETE
        [Route("{petid:int}")]
        [HttpDelete]
        [ResponseType(typeof(Pet))]
        public IHttpActionResult DeletePet(int petid)
        {
            Pet pet = db.Pets.Find(petid);
            if (pet == null)
            {
                return NotFound();
            }

            db.Pets.Remove(pet);
            db.SaveChanges();

            return Ok(pet);
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