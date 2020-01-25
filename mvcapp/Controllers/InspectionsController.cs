using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using bl;
using core;
using core.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mvcapp.Controllers
{
    public class InspectionsController : Controller
    {
        private static SQLiteConnection sqlconn = new SQLiteConnection(appconfig.sqlite);

        // GET: Inspections
        public ActionResult Index()
        {
            sqlconn.Open();
            List<Inspection> inspections = InspectionLogic.FindAll(sqlconn);
            sqlconn.Close();
            return View(inspections);
        }

        // GET: Inspections/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inspections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inspections/Create
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

        // GET: Inspections/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inspections/Edit/5
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

        // GET: Inspections/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inspections/Delete/5
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