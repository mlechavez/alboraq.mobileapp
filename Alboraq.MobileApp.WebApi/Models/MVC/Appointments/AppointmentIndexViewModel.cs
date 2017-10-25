using System.Collections.Generic;
using Alboraq.MobileApp.Core.Entities;

namespace Alboraq.MobileApp.WebApi.Models.MVC.Appointments
{
    public class AppointmentIndexViewModel
    {
        public List<Appointment> UnconfirmedAppointments { get; set; }
    }
}