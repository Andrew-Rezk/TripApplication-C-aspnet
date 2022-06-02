using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TripApplication.Models;

namespace TripApplication.Controllers
{
    public class TripController : Controller
    {
        // GET: Trip/List
        public ActionResult List()
        {
            //objective: communicate with trip data api to retrieve a list of animals
            // curl https://localhost:44326/api/TripData/ListTrips
            HttpClient client = new HttpClient() { };
            string url = "https://localhost:44326/api/TripData/ListTrips";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("the response code is");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<Trip> trips = response.Content.ReadAsAsync<IEnumerable<Trip>>().Result;


            return View(trips);
        }

        // GET: Trip/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Trip/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trip/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
