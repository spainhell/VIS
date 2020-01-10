using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coreapp.models;

namespace testapp.models
{
    public class VehicleModel
    {
        public int Id { get; set; }
        [Required]
        public VehicleTypeModel VehicleType { get; set; }
        [Required]
        public VehicleBrandModel VehicleBrand { get; set; }
        
        [DisplayName("Název")]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Vin { get; set; }
        [Required]
        public string LicensePlate { get; set; }
        [Required]
        public short Vintage { get; set; }
        [Required]
        public DateTime PurchasedOn { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public UserDriverModel Driver { get; set; }
        [Required]
        public UserBossModel Boss { get; set; }

        public int? VehicleTypeId { get; set; }
        public int? VehicleBrandId { get; set; }
        public int? DriverId { get; set; }
        public int? BossId { get; set; }

        public override string ToString()
        {
            return $"ID VOZIDLA: {Id}\nNázev: {Title}\nSPZ:  {LicensePlate}";
        }
    }
}
