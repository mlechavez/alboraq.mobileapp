using System;
using System.ComponentModel.DataAnnotations;

namespace Alboraq.MobileApp.WebApi.Areas.Admin.Models
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