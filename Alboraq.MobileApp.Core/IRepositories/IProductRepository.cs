using Alboraq.MobileApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName);        
    }
}
