using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using core.dbmappers;
using core.models;

namespace core.logic
{
    public class InspectionLogic
    {
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

        public static bool Insert(SQLiteConnection conn, Inspection i)
        {
            int result = InspectionDbMapper.Insert(conn, i);
            return (result==0);
        }

        public static void Delete(SQLiteConnection conn, int id)
        {
            List<Notification> notifications = NotificationDbMapper.SelectByInspectionId(conn, id);
            foreach (Notification notify in notifications)
            {
                NotificationDbMapper.Delete(conn, notify.Id);
            }
        }

        
    }
}
