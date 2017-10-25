using Alboraq.MobileApp.Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Helpers
{
    public interface IProductService
    {
        Task<List<ProductCategoryModel>> GetCategoriesAsync();
        Task<List<ProductModel>> GetProductsByCategoryNameAsync(string categoryName);
        Task<ProductModel> GetProductDetailAsync(string productNo);
    }
}
