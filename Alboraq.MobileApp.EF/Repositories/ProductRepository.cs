using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.Repositories
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AlboraqAppContext ctx) : base(ctx)
        {
        }

        public Task<List<Product>> GetProductsByCategoryNameAsync(string categoryName)
        {
            return Set.Where(x => x.ProductCategory.CategoryName == categoryName).OrderBy(x=>x.ProductNo).ToListAsync();
        }
    }
}
