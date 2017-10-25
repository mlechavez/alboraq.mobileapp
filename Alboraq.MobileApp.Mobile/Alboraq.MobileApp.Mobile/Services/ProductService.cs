using Alboraq.MobileApp.Mobile.Helpers;
using Alboraq.MobileApp.Mobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Alboraq.MobileApp.Mobile.Services
{
    public class ProductService : IProductService
    {
        private const string baseUri = "http://10.0.2.2:8085";

        public async Task<List<ProductCategoryModel>> GetCategoriesAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/api/product/getcategories");            

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(App.AppCredentials.TokenType, App.AppCredentials.AccessToken);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var productCategories = JsonConvert.DeserializeObject<List<ProductCategoryModel>>(content);
            return productCategories;
        }

        public async Task<ProductModel> GetProductDetailAsync(string productNo)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/api/product/getproductsbyproductno?productNo={productNo}");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(App.AppCredentials.TokenType, App.AppCredentials.AccessToken);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductModel>(content);
            return product;
        }

        public async Task<List<ProductModel>> GetProductsByCategoryNameAsync(string categoryName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/api/product/getproductsbycategoryname?categoryName={categoryName}");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(App.AppCredentials.TokenType, App.AppCredentials.AccessToken);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            var productList = JsonConvert.DeserializeObject<List<ProductModel>>(content);
            return productList;
        }
    }
}
