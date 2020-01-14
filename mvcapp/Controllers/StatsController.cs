﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using coreapp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testapp.dbmappers;
using testapp.models;

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
            List<InspectionModel> inspections = InspectionDbMapper.SelectAllByVehicleAdminId(sqlconn, 1);
            sqlconn.Close();

            int inspectionsCount = 0;
            foreach (InspectionModel inspection in inspections)
            {
                int days = (inspection.ValidTo - DateTime.Now).Days;
                if (days <= 30) inspectionsCount++;
            }

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