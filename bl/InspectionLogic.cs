using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using core.dbmappers;
using core.models;

namespace bl
{
    public class InspectionLogic
    {
        public static List<Inspection> FindAll(SQLiteConnection conn)
        {
            return InspectionDbMapper.SelectAll(conn);
        }

        public static Inspection GetLastInspection(SQLiteConnection conn, int vehicleId)
        {
            var inspections = InspectionDbMapper.SelectAllByVehicleId(conn, vehicleId);
            List<Inspection> sortedInspections = inspections.OrderByDescending(c => c.InspectionDate).ToList();

            if (sortedInspections.Count == 0) return null;
            return sortedInspections[0];
        }

        /// <summary>
        /// Připraví novou prohlídku z údajů staré prohlídky
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="vehicleId"></param>
        /// <returns></returns>
        public static Inspection PrepareNewInspection(SQLiteConnection conn, int vehicleId)
        {
            var lastInspection = GetLastInspection(conn, vehicleId);
            if (lastInspection == null) return null;
            var newInspection = new Inspection()
            {
                Defects = "", 
                Id = -1, 
                InspectionDate = DateTime.Today, 
                InspectionStation = lastInspection.InspectionStation, 
                Price = lastInspection.Price,
                ProtocolNumber = "", 
                Tachometer = 0, 
                ValidTo = (DateTime.Today).AddYears(1).AddDays(-1), 
                Vehicle = lastInspection.Vehicle
            };
            return newInspection;
        }

        public static bool Insert(SQLiteConnection conn, Inspection i, out string error)
        {
            if (i.InspectionDate >= i.ValidTo)
            {
                error = "Chybně vyplněné datum platnosti STK.";
                return false;
            }

            if (i.Price < 0)
            {
                error = "Chybně vyplněná cena STK.";
                return false;
            }

            int result = InspectionDbMapper.Insert(conn, i);
            if (result != 0)
            {
                error = "Prohlídku se nepodařilo uložit do DB.";
            }

            error = "";
            return true;
        }

        public static void Delete(SQLiteConnection conn, int id)
        {
            List<Notification> notifications = NotificationDbMapper.SelectByInspectionId(conn, id);
            foreach (Notification notify in notifications)
            {
                NotificationDbMapper.Delete(conn, notify.Id);
            }
        }

        public static int EndingInspectionsCount(SQLiteConnection conn, int adminId, int remainDays)
        {
            // vypíše všechny prohlídky u vozidel daného správce, které končí u určené lhůtě
            List<Inspection> inspections = InspectionDbMapper.SelectAllByVehicleAdminId(conn, 1);

            int inspectionsCount = 0;
            foreach (Inspection inspection in inspections)
            {
                int days = (inspection.ValidTo - DateTime.Now).Days;
                if (days <= remainDays) inspectionsCount++;
            }

            return inspectionsCount;
        }
    }
}
