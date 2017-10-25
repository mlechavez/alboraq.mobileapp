using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alboraq.MobileApp.Core.Entities
{
    public class Product
    {
        private ICollection<OrderDetail> _orderDetails;

        public int ID { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Qoh { get; set; }
        public string ImageUrl { get; set; }
        public int? ProductCategoryID { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails ?? (_orderDetails = new List<OrderDetail>()); }
            set { _orderDetails = value; }
        }

        [JsonIgnore]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
