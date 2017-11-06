using Alboraq.MobileApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core.IRepositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersWithoutInvoice();
        Task<List<Order>> GetOrdersWithInvoice();
    }
}
