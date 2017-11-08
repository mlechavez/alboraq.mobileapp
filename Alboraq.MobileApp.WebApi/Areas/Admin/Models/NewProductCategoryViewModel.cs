using Alboraq.MobileApp.Core.Entities;
using System.Collections.Generic;

namespace Alboraq.MobileApp.WebApi.Areas.Admin.Models
{
    public class NewProductCategoryViewModel
    {
        public List<ProductCategory> ProductCategories { get; set; }
        public ProductCategoryModel ProductCategoryModel { get; set; }
    }
}