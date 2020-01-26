using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using bl;
using core;
using core.dbmappers;
using core.models;
using core.xmlmappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace mvcapp.Controllers
{
    public class VehiclesController : Controller
    {
        private static SQLiteConnection sqlconn = new SQLiteConnection(appconfig.sqlite);

        // GET: Vehicles
        public ActionResult Index()
        {
            sqlconn.Open();
            List<Vehicle> vehicles = VehicleLogic.FindAll(sqlconn);
            sqlconn.Close();

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int id)
        {
            sqlconn.Open();
            Vehicle vehicle = VehicleLogic.FindById(sqlconn, id);
            sqlconn.Close();
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            sqlconn.Open();
            List<VehicleType> types = VehicleTypeXmlMapper.SelectAll();
            List<VehicleBrand> brands = VehicleBrandXmlMapper.SelectAll();

            List<UserDriver> drivers = UserDriverLogic.FindAll(sqlconn);
            List<UserBoss> bosses = UserBossLogic.FindAll(sqlconn);

            sqlconn.Close();

            ViewData["vehicleTypes"] = types;
            ViewData["vehicleBrands"] = brands;
            ViewData["drivers"] = drivers;
            ViewData["bosses"] = bosses;

            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vehicle vehicle)
        {
            try
            {
                var r = VehicleLogic.Create(sqlconn, vehicle, vehicle.VehicleTypeId.Value, vehicle.VehicleBrandId.Value, 4, 1);

                if (!r) return View();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                sqlconn.Close();
                return View();
            }
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Vehicles/Edit/5
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

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int id)
        {
            sqlconn.Open();
            Vehicle vehicle = VehicleLogic.FindById(sqlconn, id);
            sqlconn.Close();

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Vehicle vehicle)
        {
            try
            {
                sqlconn.Open();
                List<Inspection> inspections = InspectionLogic.FindAllByVehicleId(sqlconn, id);
                sqlconn.Close();

                if (inspections.Count > 0) return View(vehicle);

                sqlconn.Open();
                VehicleLogic.Delete(sqlconn, 1, vehicle.Id);
                sqlconn.Close();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(vehicle);
            }
        }
    }
}