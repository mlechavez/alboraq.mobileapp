using Newtonsoft.Json;
using System.Collections.Generic;

namespace Alboraq.MobileApp.Core.Entities
{
    public class ProductCategory
    {
        private ICollection<Product> _products;

        public int ProductCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string ImageUrl { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products
        {
            get { return _products ?? (_products = new List<Product>()); }
            set { _products = value; }
        }
    }
}
