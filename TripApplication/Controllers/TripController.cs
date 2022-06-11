using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TripApplication.Models;

namespace TripApplication.Controllers
{
    public class TripController : Controller
    {
        private static readonly HttpClient client;

        static TripController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44326/api/TripData/");
        }
        // GET: Trip/List
        public ActionResult List()
        {
            //objective: communicate with trip data api to retrieve a list of trips
            // curl https://localhost:44326/api/TripData/ListTrips
            
            string url = "listtrips";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<Trip> trips = response.Content.ReadAsAsync<IEnumerable<Trip>>().Result;

            return View(trips);
        }

        // GET: Trip/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with trip data api to retrieve one trip
            // curl https://localhost:44326/api/TripData/findtrip/{id}
            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44326/api/TripData/findtrip/" +id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            TripDto selectedtrip = response.Content.ReadAsAsync<TripDto>().Result;

            return View(selectedtrip);
        }

        // GET: Trip/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Trip/Create
        [HttpPost]
        public ActionResult Create(Trip trip)
        {
            string url = "https://localhost:44326/api/TripData/addtrip";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(trip);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            
            client.PostAsync(url, content);

            return RedirectToAction("list");
        }

        // GET: Trip/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Trip/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Trip/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Trip/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
