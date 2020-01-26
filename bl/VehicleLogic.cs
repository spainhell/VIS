using System.Collections.Generic;
using System.Data.SQLite;
using core.dbmappers;
using core.models;
using core.xmlmappers;

namespace bl
{
    public class VehicleLogic
    {
        public static List<Vehicle> FindAll(SQLiteConnection conn)
        {
            return VehicleDbMapper.SelectAll(conn);
        }

        public static Vehicle FindById(SQLiteConnection conn, int id)
        {
            return VehicleDbMapper.SelectById(conn, id);
        }

        public static bool Create(SQLiteConnection conn, Vehicle vh, int typeId, int brandId, int driverId, int bossId)
        {
            // kontrola existence VIN v databázi
            bool vinExists = VehicleDbMapper.VinExists(conn, vh.Vin);
            if (vinExists) return false;

            vh.VehicleType = VehicleTypeXmlMapper.SelectById(typeId);
            vh.VehicleBrand = VehicleBrandXmlMapper.SelectById(brandId);
            vh.Driver = UserDriverDbMapper.SelectById(conn, 4);
            vh.Boss = UserBossDbMapper.SelectById(conn, 1);

            int result = VehicleDbMapper.Insert(conn, vh);

            return true;
        }

        public static bool Delete(SQLiteConnection conn, int adminId, int vehicleId)
        {
            // má uživatel oprávnění?
            UserAdmin ua = UserAdminDbMapper.SelectById(conn, adminId);
            if (ua == null) return false;

            // smazání všech prohlídek
            List<Inspection> inspections = InspectionDbMapper.SelectAllByVehicleId(conn, vehicleId);
            foreach (Inspection inspection in inspections)
            {
                InspectionLogic.Delete(conn, inspection.Id);
            }

            return true;
        }

        public static long CountVehiclesByAdminId(SQLiteConnection conn, int adminId)
        {
            return VehicleDbMapper.SelectVehiclesByAdminId(conn, adminId);
        }

        public static long CountDriversByAdminId(SQLiteConnection conn, int adminId)
        {
            return VehicleDbMapper.SelectDriversCountByAdminId(conn, adminId);
        }
    }
}
