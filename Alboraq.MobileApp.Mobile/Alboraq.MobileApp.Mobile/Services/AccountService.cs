using Alboraq.MobileApp.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alboraq.MobileApp.Mobile.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reactive.Linq;

namespace Alboraq.MobileApp.Mobile.Services
{
    public class AccountService : IAccountService
    {
        private readonly ICacheService _cacheService;

        public AccountService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<bool> IsLoggedIn()
        {
            var credentials = await _cacheService.GetObject<AppCredentials>("appCredentials");
            return credentials != null;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "http://10.0.2.2:8085/token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {                
                var content = await response.Content.ReadAsStringAsync();

                AppCredentials credentials = JsonConvert.DeserializeObject<AppCredentials>(content);                

                await _cacheService.InsertObject("appCredentials", credentials);
                return true;
            }
            return false;
        }

        public async Task<bool> RegisterAsync(RegisterModel registerModel)
        {
            if (registerModel == null) throw new ArgumentNullException("loginModel");
            

            var json = JsonConvert.SerializeObject(registerModel);

            var content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var client = new HttpClient();
            var responseMessage = await client.PostAsync("http://10.0.2.2:8085/api/account/register", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var isSuccess = await LoginAsync(registerModel.Email, registerModel.Password);
                if (isSuccess)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
