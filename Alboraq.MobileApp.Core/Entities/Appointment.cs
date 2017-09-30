using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core.Entities
{
    public class Appointment
    {
        public int ID { get; set; }
        public string CustomerNo { get; set; }
        public string PlateNo { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string MobileNo { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
