using Alboraq.MobileApp.Core.IRepositories;
using System;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAppointmentRepository Appointments { get; }
        IOrderDetailRepository OrderDetails { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        IProductCategoryRepository ProductCategories { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();        
    }
}
