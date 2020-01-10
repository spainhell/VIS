using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.models
{
    public class InspectionModel
    {
        public int Id { get; set; }
        public VehicleModel Vehicle { get; set; }
        public DateTime InspectionDate { get; set; }
        public DateTime ValidTo { get; set; }
        public InspectionStationModel InspectionStation { get; set; }
        public string ProtocolNumber { get; set; }
        public int Tachometer { get; set; }
        public decimal Price { get; set; }
        public string Defects { get; set; }

        public override string ToString()
        {
            return $"ID STK: {Id}, ID vozidla: {Vehicle.Id}, platnost do: {ValidTo.ToString("d")}";
        }
    }
}
