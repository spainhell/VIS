using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using bl;
using core;
using core.dbmappers;
using core.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace mvcapp.Controllers
{
    public class StatsController : Controller
    {
        private static SQLiteConnection sqlconn = new SQLiteConnection(appconfig.sqlite);

        // GET: Stats
        public ActionResult Index()
        {
            sqlconn.Open();
            long vehiclesCount = VehicleDbMapper.SelectVehiclesByAdminId(sqlconn, 1);
            long driversCount = VehicleDbMapper.SelectDriversCountByAdminId(sqlconn, 1);
            long inspectionsCount = InspectionLogic.EndingInspectionsCount(sqlconn, 1, 30);
            
            sqlconn.Close();


            ViewData["Vehicles"] = vehiclesCount;
            ViewData["Drivers"] = driversCount;
            ViewData["Inspections"] = inspectionsCount;

            return View();
        }

        // GET: Stats/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Stats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stats/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Stats/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Stats/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Stats/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Stats/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}