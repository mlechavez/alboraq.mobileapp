using Alboraq.MobileApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Helpers
{
    public interface IAppointmentService
    {
        Task<HttpResponseMessage> SetAppointmentAsync(string email, string token, DateTime appointmentDate);
    }
}
