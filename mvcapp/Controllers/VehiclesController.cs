using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
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
            List<Vehicle> vehicles = VehicleDbMapper.SelectAll();
            sqlconn.Close();

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int id)
        {
            sqlconn.Open();
            Vehicle vehicle = VehicleDbMapper.SelectById(id);
            sqlconn.Close();
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            sqlconn.Open();
            List<VehicleType> types = VehicleTypeXmlMapper.SelectAll();
            List<VehicleBrand> brands = VehicleBrandXmlMapper.SelectAll();

            List<UserDriver> drivers = UserDriverDbMapper.SelectAll();
            List<UserBoss> bosses = UserBossDbMapper.SelectAll();

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
                sqlconn.Open();
                bool vinExists = VehicleDbMapper.VinExists(vehicle.Vin);
                if (vinExists) return View(vehicle);

                vehicle.VehicleType = VehicleTypeXmlMapper.SelectById(vehicle.VehicleTypeId.Value);
                vehicle.VehicleBrand =
                    VehicleBrandXmlMapper.SelectById(vehicle.VehicleBrandId.Value);
                vehicle.Driver = UserDriverDbMapper.SelectById(vehicle.DriverId.Value);
                vehicle.Boss = UserBossDbMapper.SelectById(vehicle.BossId.Value);
                
                int result = VehicleDbMapper.Insert(vehicle);
                sqlconn.Close();

                if (result != 0) return View();

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
            Vehicle vehicle = VehicleDbMapper.SelectById(id);
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
                List<Inspection> inspections = InspectionDbMapper.SelectAllByVehicleId(id);
                sqlconn.Close();

                if (inspections.Count > 0) return View(vehicle);

                sqlconn.Open();
                VehicleDbMapper.Delete(vehicle);
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