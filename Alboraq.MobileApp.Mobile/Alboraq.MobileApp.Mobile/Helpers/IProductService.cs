﻿using Alboraq.MobileApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Helpers
{
    public interface IProductService
    {
        Task<List<ProductCategoryModel>> GetCategoriesAsync();
    }
}