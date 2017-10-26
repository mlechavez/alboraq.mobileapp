using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Alboraq.MobileApp.WebApi.Models.MVC.Appointments
{
    public class AppointmentModel
    {
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string PlateNo { get; set; }
        [Required]
        public DateTime? AppointmentDate { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
    }
}