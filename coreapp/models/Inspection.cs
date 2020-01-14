using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.models
{
    public class Inspection
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public DateTime InspectionDate { get; set; }
        public DateTime ValidTo { get; set; }
        public InspectionStation InspectionStation { get; set; }
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
