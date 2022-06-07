using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TripApplication.Models;

namespace TripApplication.Controllers
{
    public class TripDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/TripData/ListTrips
        [HttpGet]
        [Route("api/TripData/ListTrips")]
        public IEnumerable<TripDto> ListTrips()
        {
            List<Trip> Trips = db.Trips.ToList();
            List<TripDto> TripDtos = new List<TripDto>();
            Trips.ForEach(a => TripDtos.Add(new TripDto()
            {
                TripID = a.TripID,
                TripName = a.TripName,
                TripCountry = a.TripCountry,
                TripDate = a.TripDate,
                TripNotes = a.TripNotes
            }));
            return TripDtos;
        }

        // GET: api/TripData/FindTrip/5
        [ResponseType(typeof(Trip))]
        [HttpGet]
        [Route("api/TripData/FindTrip/{id}")]
        public IHttpActionResult FindTrip(int id)
        {
            Trip Trip = db.Trips.Find(id);
            TripDto TripDto = new TripDto()
            {
                TripID = Trip.TripID,
                TripName = Trip.TripName,
                TripCountry = Trip.TripCountry,
                TripDate = Trip.TripDate,
                TripNotes = Trip.TripNotes
            };
            if (Trip == null)
            {
                return NotFound();
            }

            return Ok(TripDto);
        }

        // POST: api/TripData/UpdateTrip/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateTrip(int id, Trip trip)
        {
            Debug.WriteLine("I have reached the update trip");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model state is invalid");
                return BadRequest(ModelState);
            }

            if (id != trip.TripID)
            {
                Debug.WriteLine("ID mismatch");
                return BadRequest();
            }

            db.Entry(trip).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
                {
                    return NotFound();
                    Debug.WriteLine("trip not found");
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("none of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TripData/AddTrip
        [ResponseType(typeof(Trip))]
        [HttpPost]
        public IHttpActionResult AddTrip(Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Trips.Add(trip);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = trip.TripID }, trip);
        }

        // POST: api/TripData/DeleteTrip/5
        [ResponseType(typeof(Trip))]
        [HttpPost]
        public IHttpActionResult DeleteTrip(int id)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            db.Trips.Remove(trip);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TripExists(int id)
        {
            return db.Trips.Count(e => e.TripID == id) > 0;
        }
    }
}