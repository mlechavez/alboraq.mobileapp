using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Core.Entities
{
    public class ProductCategory
    {
        private ICollection<Product> _products;

        public int ProductCategoryID { get; set; }
        public string CategoryName { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Product> Products
        {
            get { return _products ?? (_products = new List<Product>()); }
            set { _products = value; }
        }
    }
}
