using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public virtual ICollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails ?? (_orderDetails = new List<OrderDetail>()); }
            set { _orderDetails = value; }
        }
    }
}
