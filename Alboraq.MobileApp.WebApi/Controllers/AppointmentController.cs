using Alboraq.MobileApp.Core;
using Alboraq.MobileApp.WebApi.Models.MVC.Appointments;
using System.Threading.Tasks;
using System.Web.Http;

namespace Alboraq.MobileApp.WebApi.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : ApiController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWork _uow;
        
        public AppointmentController(ApplicationUserManager userManager, IUnitOfWork uow)
        {
            _userManager = userManager;
            _uow = uow;
        }                

        [Route("NewAppointment")]        
        public async Task<IHttpActionResult> NewAppointment([FromBody] AppointmentModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null)
            {
                return NotFound();
            }

            var appointment = new Core.Entities.Appointment
            {
                CustomerName = user.Name,
                PlateNo = user.PlateNo,
                AppointmentDate = model.AppointmentDate,
                MobileNo = user.PhoneNumber,
                Email = user.Email
            };
            
            _uow.Appointments.Add(appointment);
            await _uow.SaveChangesAsync();

            await _userManager.SendAppEmailAsync("New Appointment", "new appointment has been added check online", "echavez.marklester@boraq-porsche.com.qa");
            
            return Ok(appointment);
        }
    }
}
