using System;
using System.Collections.Generic;
using System.Text;
using core.dbmappers;
using core.models;
using core.xmlmappers;

namespace core.logic
{
    public class VehicleLogic
    {
        public static bool Create(Vehicle vh)
        {
            bool vinExists = VehicleDbMapper.VinExists(vh.Vin);
            if (vinExists) return false;

            vh.VehicleType = VehicleTypeXmlMapper.SelectById(vh.VehicleTypeId.Value);
            vh.VehicleBrand = VehicleBrandXmlMapper.SelectById(vh.VehicleBrandId.Value);
            vh.Driver = UserDriverDbMapper.SelectById(vh.DriverId.Value);
            vh.Boss = UserBossDbMapper.SelectById(vh.BossId.Value);

            int result = VehicleDbMapper.Insert(vh);

            return true;
        }

        public static void Delete(int id)
        {
            // máme oprávnění?

            List<Inspection> inspections = InspectionDbMapper.SelectAllByVehicleId(id);
            foreach (Inspection inspection in inspections)
            {
                InspectionLogic.Delete(inspection.Id);
            }
        }

    }
}
