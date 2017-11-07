using Alboraq.MobileApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alboraq.MobileApp.WebApi.Models.MVC.Prod
{
    public class NewProductCategoryViewModel
    {
        public List<ProductCategory> ProductCategories { get; set; }
        public ProductCategoryModel ProductCategoryModel { get; set; }
    }
}