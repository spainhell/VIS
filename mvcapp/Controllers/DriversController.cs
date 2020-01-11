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

namespace mvcapp.Controllers
{
    public class DriversController : Controller
    {
        private static SQLiteConnection sqlconn = new SQLiteConnection(appconfig.sqlite);

        // GET: Drivers
        public ActionResult Index()
        {
            sqlconn.Open();
            List<UserDriverModel> drivers = UserDriverDbMapper.SelectAll(sqlconn);
            sqlconn.Close();
            return View(drivers);
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int id)
        {
            sqlconn.Open();
            UserDriverModel driver = UserDriverDbMapper.SelectById(sqlconn, id);
            sqlconn.Close();
            return View(driver);
        }

        // GET: Drivers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserDriverModel driver)
        {
            try
            {
                sqlconn.Open();
                int result = UserDriverDbMapper.Insert(sqlconn, driver);
                sqlconn.Close();

                if (result != 0) throw new Exception();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(driver);
            }
        }

        // GET: Drivers/Edit/5
        public ActionResult Edit(int id)
        {
            sqlconn.Open();
            UserDriverModel driver = UserDriverDbMapper.SelectById(sqlconn, id);
            sqlconn.Close();
            return View(driver);
        }

        // POST: Drivers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserDriverModel driver)
        {
            try
            {
                sqlconn.Open();
                int result = UserDriverDbMapper.Update(sqlconn, driver);
                sqlconn.Close();
                
                if (result != 0) throw new Exception();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(driver);
            }
        }

        // GET: Drivers/Delete/5
        public ActionResult Delete(int id)
        {
            sqlconn.Open();
            UserDriverModel driver = UserDriverDbMapper.SelectById(sqlconn, id);
            sqlconn.Close();

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserDriverModel driver)
        {
            try
            {
                sqlconn.Open();
                int result = UserDriverDbMapper.Update(sqlconn, driver);
                sqlconn.Close();

                if (result != 0) throw new Exception();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(driver);
            }
        }
    }
}