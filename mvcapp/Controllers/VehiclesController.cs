using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using coreapp;
using coreapp.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testapp.dbmappers;
using testapp.models;
using testapp.xmlmappers;

namespace mvcapp.Controllers
{
    public class VehiclesController : Controller
    {
        private static SQLiteConnection sqlconn = new SQLiteConnection(appconfig.sqlite);

        // GET: Vehicles
        public ActionResult Index()
        {
            sqlconn.Open();
            List<VehicleModel> vehicles = VehicleDbMapper.SelectAll(sqlconn);
            sqlconn.Close();

            return View(vehicles);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int id)
        {
            sqlconn.Open();
            VehicleModel vehicle = VehicleDbMapper.SelectById(sqlconn, id);
            sqlconn.Close();
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            sqlconn.Open();
            List<VehicleTypeModel> types = VehicleTypeXmlMapper.SelectAll(appconfig.xmlTypes);
            List<VehicleBrandModel> brands = VehicleBrandXmlMapper.SelectAll(appconfig.xmlBrands);

            List<UserDriverModel> drivers = UserDriverDbMapper.SelectAll(sqlconn);
            List<UserBossModel> bosses = UserBossDbMapper.SelectAll(sqlconn);

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
        public ActionResult Create(VehicleModel vehicle)
        {
            try
            {
                sqlconn.Open();
                bool vinExists = VehicleDbMapper.VinExists(sqlconn, vehicle.Vin);
                if (vinExists) return View(vehicle);

                vehicle.VehicleType = VehicleTypeXmlMapper.SelectById(appconfig.xmlTypes, vehicle.VehicleTypeId.Value);
                vehicle.VehicleBrand =
                    VehicleBrandXmlMapper.SelectById(appconfig.xmlBrands, vehicle.VehicleBrandId.Value);
                vehicle.Driver = UserDriverDbMapper.SelectById(sqlconn, vehicle.DriverId.Value);
                vehicle.Boss = UserBossDbMapper.SelectById(sqlconn, vehicle.BossId.Value);
                
                int result = VehicleDbMapper.Insert(sqlconn, vehicle);
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
            VehicleModel vehicle = VehicleDbMapper.SelectById(sqlconn, id);
            sqlconn.Close();

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VehicleModel vehicle)
        {
            try
            {
                sqlconn.Open();
                List<InspectionModel> inspections = InspectionDbMapper.SelectAllByVehicleId(sqlconn, id);
                sqlconn.Close();

                if (inspections.Count > 0) return View(vehicle);

                sqlconn.Open();
                VehicleDbMapper.Delete(sqlconn, vehicle);
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