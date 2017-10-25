using Alboraq.MobileApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core.IRepositories
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<List<Appointment>> GetUnconfirmedAppointmentsAsync();
        Task<List<Appointment>> GetConfirmedAppointmentsAsync();        
    }
}
