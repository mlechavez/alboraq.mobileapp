﻿using Alboraq.MobileApp.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alboraq.MobileApp.Mobile.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Akavache;
using System.Reactive.Linq;

namespace Alboraq.MobileApp.Mobile.Services
{
    public class AccountService : IAccountService
    {        

        public AccountService()
        {
            BlobCache.ApplicationName = "AlboraqApp";
            BlobCache.EnsureInitialized();
        }

        public async Task<HttpResponseMessage> LoginAsync(string username, string password)
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
                var responseContent = await response.Content.ReadAsStringAsync();

                AppCredentialsModel login = JsonConvert.DeserializeObject<AppCredentialsModel>(responseContent);
                await BlobCache.Secure.InsertObject("login", login);
            }
            
            return response;
        }

        public async Task<HttpResponseMessage> RegisterAsync(RegisterModel registerModel)
        {
            if (registerModel == null) throw new ArgumentNullException("loginModel");
            
            var json = JsonConvert.SerializeObject(registerModel);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "http://10.0.2.2:8085/api/account/register");

            request.Content = content;

            
            var client = new HttpClient();
            var response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                response = await LoginAsync(registerModel.Email, registerModel.Password);              
            }            
            return response;
        }       
    }
}
