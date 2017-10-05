using Alboraq.MobileApp.Core;
using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.WebApi.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Alboraq.MobileApp.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/appointment")]
    public class AppointmentController : ApiController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _uow;
        public AppointmentController()
        {
        }

        public AppointmentController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }        

        [HttpPost]
        [Route("newappointment")]
        public async Task<IHttpActionResult> NewAppointment(Appointment appointment)
        {
            var user = await UserManager.FindByEmailAsync(appointment.Email);

            if (user == null)
            {
                return NotFound();
            }
            appointment.CustomerName = user.Name;
            appointment.PlateNo = user.PlateNo;
            appointment.MobileNo = user.PhoneNumber;
            
            _uow.Appointments.Add(appointment);
            await _uow.SaveChangesAsync();

            await UserManager.SendAppEmailAsync("New Appointment", "new appointment has been added check online", "echavez.marklester@boraq-porsche.com.qa");
            
            return Ok(appointment);
        }
    }
}
