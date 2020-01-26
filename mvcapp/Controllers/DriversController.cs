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
    public class DriversController : Controller
    {
        private static SQLiteConnection sqlconn = new SQLiteConnection(appconfig.sqlite);

        // GET: Drivers
        public ActionResult Index()
        {
            sqlconn.Open();
            List<UserDriver> drivers = UserDriverLogic.FindAll(sqlconn);
            sqlconn.Close();
            return View(drivers);
        }

        // GET: Drivers/Details/5
        public ActionResult Details(int id)
        {
            sqlconn.Open();
            UserDriver driver = UserDriverLogic.FindById(sqlconn, id);
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
        public ActionResult Create(UserDriver driver)
        {
            try
            {
                sqlconn.Open();
                int result = UserDriverLogic.Insert(sqlconn, driver);
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
            UserDriver driver = UserDriverLogic.FindById(sqlconn, id);
            sqlconn.Close();
            return View(driver);
        }

        // POST: Drivers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserDriver driver)
        {
            try
            {
                sqlconn.Open();
                int result = UserDriverLogic.Update(sqlconn, driver);
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
            UserDriver driver = UserDriverLogic.FindById(sqlconn, id);
            sqlconn.Close();

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserDriver driver)
        {
            try
            {
                sqlconn.Open();
                int result = UserDriverLogic.Update(sqlconn, driver);
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