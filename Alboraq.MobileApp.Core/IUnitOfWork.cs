using Alboraq.MobileApp.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderDetailRepository OrderDetails { get; }
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }

        int SaveChanges();
        Task<int> SaveChangesAsync();        
    }
}
