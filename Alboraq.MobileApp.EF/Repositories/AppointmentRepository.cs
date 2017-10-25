using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.Core.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.Repositories
{
    internal class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AlboraqAppContext ctx) : base(ctx)
        {
        }

        public Task<List<Appointment>> GetUnconfirmedAppointmentsAsync()
        {
            return Set.Where(appt => !appt.IsConfirmed)
               .OrderByDescending(appt => appt.AppointmentDate)
               .ToListAsync();
        }

        public Task<List<Appointment>> GetConfirmedAppointmentsAsync()
        {
            return Set.Where(appt => appt.IsConfirmed)
               .OrderByDescending(appt => appt.DateConfirmed)
               .ToListAsync();
        }                
    }
}
