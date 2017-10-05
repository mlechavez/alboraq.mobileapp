using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Models
{
    public class AppCredentialsModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("userName")]
        public string Username { get; set; }
        [JsonProperty(".issued")]
        public DateTime? DateIssued { get; set; }
        [JsonProperty(".expires")]
        public DateTime? DateExpires { get; set; }
    }
}
