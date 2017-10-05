using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Models
{
    public class AppointmentModel
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        [JsonProperty]
        public string PlateNo { get; set; }
        public DateTime? AppointmentDate { get; set; }
        [JsonProperty]
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
