using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core.Entities
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public string OrderNo { get; set; }
        public string ProductNo { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}
