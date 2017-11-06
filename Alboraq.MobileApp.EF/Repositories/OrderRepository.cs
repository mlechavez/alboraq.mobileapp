using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.Core.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.Repositories
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AlboraqAppContext ctx) : base(ctx)
        {
        }

        public Task<List<Order>> GetOrdersWithInvoice()
        {
            return Set.Where(order => !string.IsNullOrWhiteSpace(order.InvoiceNo))               
               .OrderByDescending(order => order.OrderDate)
               .ToListAsync();
        }

        public Task<List<Order>> GetOrdersWithoutInvoice()
        {
            return Set.Where(order => string.IsNullOrWhiteSpace(order.InvoiceNo))
                
               .OrderByDescending(order => order.OrderDate)
               .ToListAsync();
        }
    }
}
