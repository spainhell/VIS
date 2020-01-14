using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.models
{
    public class Notification
    {
        public int Id { get; set; }
        public Inspection Inspection { get; set; }
        public string Destination { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime Delivered { get; set; }
    }
}
