using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.models
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public InspectionModel Inspection { get; set; }
        public string Destination { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime Delivered { get; set; }
    }
}
