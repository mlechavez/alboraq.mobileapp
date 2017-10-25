using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alboraq.MobileApp.WebApi.Models.MVC.Appointments
{
    public class AppointmentModel
    {
        public string CustomerName { get; set; }
        public string PlateNo { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
    }
}