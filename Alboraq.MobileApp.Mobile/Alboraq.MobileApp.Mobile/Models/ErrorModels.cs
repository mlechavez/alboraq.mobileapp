using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Models
{
    public class ErrorRegister
    {
        [JsonProperty("Message")]
        public string Message { get; set; }
        [JsonProperty("ModelState")]
        public IDictionary<string,ICollection<string>> ModelState { get; set; }
    }

    public class ErrorLogin
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string Description { get; set; }
    }
}
