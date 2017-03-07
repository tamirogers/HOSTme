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
using GroupProject.Infrastructure;
using GroupProject.Models;

namespace GroupProject.Controllers
{
    public class RoomsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Rooms
        public IQueryable<Room> GetRooms()
        {
            return db.Rooms;
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        } 
        
        // Get Rooms by Search Criteria
        [Route("api/Rooms/Search")]
        [HttpGet]
        public IQueryable<Room> SearchRooms([FromUri]SearchRoom search)
        {
            IQueryable<Room> allRooms = db.Rooms;

            if(search.City != null)
            {
                allRooms = allRooms.Where(r => r.City == search.City);
            }

            if(search.Zip != null)
            {
                allRooms = allRooms.Where(r => r.Zip == search.Zip);
            }

            if(search.MaxPrice >0)
            {
                allRooms = allRooms.Where(r => r.Price <= search.MaxPrice);
            }

            if(search.GuestLimit > 0)
            {
                allRooms = allRooms.Where(r => r.GuestLimit <= search.GuestLimit);
            }

            if(search.KeyWord != null)
            {
                allRooms = allRooms.Where(r => r.Description.Contains(search.KeyWord));
            }

            if(search.Private == true)
            {
                allRooms = allRooms.Where(r => r.Private == true);
            }

            return allRooms;



        }

        

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.RoomId)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
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

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = room.RoomId }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            //delete all favorites with the given room id

            var existingFavs = from r in db.Favorites
                               where r.RoomId == id
                               select r;

            foreach (Favorite f in existingFavs)
            {
                db.Favorites.Remove(f);
                
            }


            db.Rooms.Remove(room);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Rooms.Count(e => e.RoomId == id) > 0;
        }
    }
}