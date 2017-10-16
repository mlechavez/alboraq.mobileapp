using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.Core.IRepositories;

namespace Alboraq.MobileApp.EF.Repositories
{
    internal class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(AlboraqAppContext ctx) : base(ctx)
        {
        }
    }
}
