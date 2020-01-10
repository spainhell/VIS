using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public VehicleTypeModel VehicleType { get; set; }
        public VehicleBrandModel VehicleBrand { get; set; }
        public string Title { get; set; }
        public string Vin { get; set; }
        public string LicensePlate { get; set; }
        public short Vintage { get; set; }
        public DateTime PurchasedOn { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"ID VOZIDLA: {Id}\nNázev: {Title}\nSPZ:  {LicensePlate}";
        }
    }
}
