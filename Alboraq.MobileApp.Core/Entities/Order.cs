using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core.Entities
{
    public class Order
    {
        private ICollection<OrderDetail> _orderDetails;

        public int ID { get; set; }
        public string OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CustomerNo { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails
        {
            get { return _orderDetails ?? (_orderDetails = new List<OrderDetail>()); }
            set { _orderDetails = value; }
        }
    }
}
