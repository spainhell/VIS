using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace core.models
{
    public class InspectionStation
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string StationNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Orp { get; set; }
        public string District { get; set; }
        public string Region { get; set; }
        public string Url { get; set; }
    }
}
