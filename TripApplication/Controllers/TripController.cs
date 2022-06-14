using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TripApplication.Models;
using TripApplication.Models.ViewModels;

namespace TripApplication.Controllers
{
    public class TripController : Controller
    {
        private static readonly HttpClient client;
        JavaScriptSerializer jss = new JavaScriptSerializer();

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
            string url = "https://localhost:44326/api/TripData/findtrip/" + id;
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
            string url = "https://localhost:44326/api/TripData/findtrip/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TripDto selectedtrip = response.Content.ReadAsAsync<TripDto>().Result;
            UpdateTrip trip = new UpdateTrip();
            trip.SelectedTrip = selectedtrip;
            return View(trip);
        }

        // POST: Trip/update/5
        [HttpPost]
        public ActionResult update(int id, Trip trip)
        {
            string url = "https://localhost:44326/api/TripData/updatetrip/" + id;
            string jsonpayload = jss.Serialize(trip);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("list");
        }

        // GET: Trip/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "https://localhost:44326/api/TripData/findtrip/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            TripDto selectedtrip = response.Content.ReadAsAsync<TripDto>().Result;
            return View(selectedtrip);

        }

        // POST: Trip/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "https://localhost:44326/api/TripData/deletetrip/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            return RedirectToAction("List");
        }
    }
}
