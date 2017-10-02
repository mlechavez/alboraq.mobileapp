﻿using Alboraq.MobileApp.Core.Entities;
using Alboraq.MobileApp.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.EF.Repositories
{
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AlboraqAppContext ctx) : base(ctx)
        {
        }
    }
}
