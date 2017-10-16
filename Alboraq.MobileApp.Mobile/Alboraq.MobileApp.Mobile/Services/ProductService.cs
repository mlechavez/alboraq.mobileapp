using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Services
{
    public class ProductService : IProductService
    {
        private const string baseUri = "http://10.0.2.2:8085";

        public async Task<List<ProductCategoryModel>> GetCategoriesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/api/product/getcategories");            

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var productCategories = JsonConvert.DeserializeObject<List<ProductCategoryModel>>(content);
            return productCategories;
        }
    }
}
