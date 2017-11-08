using System.Collections.Generic;
using Alboraq.MobileApp.Core.Entities;

namespace Alboraq.MobileApp.WebApi.Areas.Admin.Models
{
    public class AppointmentIndexViewModel
    {
        public List<Appointment> UnconfirmedAppointments { get; set; }
    }
}