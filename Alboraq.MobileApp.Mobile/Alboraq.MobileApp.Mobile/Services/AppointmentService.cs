using Akavache;
using Alboraq.MobileApp.Mobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Diagnostics;
using Alboraq.MobileApp.Mobile.Helpers;
using System.Net.Http.Headers;

namespace Alboraq.MobileApp.Mobile.Services
{
    public class AppointmentService : IAppointmentService
    {
        private const string baseUri = "http://10.0.2.2:8085";

        public AppointmentService()
        {            
        }
        public async Task<HttpResponseMessage> SetAppointmentAsync(string email, string token, AppointmentModel model)
        {                        
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");            
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUri}/api/appointment/newappointment")
            {
                Content = content
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);

            return response;
        }
    }
}
