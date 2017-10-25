using Alboraq.MobileApp.Core;
using Alboraq.MobileApp.WebApi.Models.MVC.Appointments;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Alboraq.MobileApp.WebApi.Controllers.MVC
{
    [RoutePrefix("appointments")]
    public class ApptController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ApptController(IUnitOfWork unitOfWork)
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

            await _unitOfWork.SaveChangesAsync();

            return Json(new { message = "Appointment has been confirmed!" });
        }
    }
}