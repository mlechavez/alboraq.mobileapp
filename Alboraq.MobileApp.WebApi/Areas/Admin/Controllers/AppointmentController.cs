using Alboraq.MobileApp.Core;
using Alboraq.MobileApp.WebApi.Areas.Admin.Models;
using Alboraq.MobileApp.WebApi.Filters;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Alboraq.MobileApp.WebApi.Areas.Admin.Controllers
{    
    [AccessActionFilter(RoleName = "receptionist")]
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: appointments/
        [Route("")]
        public async Task<ActionResult> Index()
        {
            var unconfirmedAppointments = await
                _unitOfWork.Appointments.GetUnconfirmedAppointmentsAsync();

            var viewModel = new AppointmentIndexViewModel
            {
                UnconfirmedAppointments = unconfirmedAppointments
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("confirmAppointment")]
        public async Task<ActionResult> ConfirmAppointment(int appointmentID)
        {
            var appointment = await _unitOfWork.Appointments.GetAsync(appointmentID);

            if (appointment == null)
            {
                return Json(new { errorMessage = "Appointment cannot be found!!" });
            }

            appointment.IsConfirmed = true;
            appointment.DateConfirmed = DateTime.UtcNow;
            appointment.ConfirmedBy = User.Identity.Name;

            await _unitOfWork.SaveChangesAsync();

            //TO DO: SEND EMAIL AND SMS HERE

            return Json(new { message = "Appointment has been confirmed!" });
        }
    }
}