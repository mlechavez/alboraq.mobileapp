using Alboraq.MobileApp.Core.Entities;
using System.Collections.Generic;

namespace Alboraq.MobileApp.WebApi.Areas.Admin.Models
{
    public class OrderListViewModel
    {
        public List<Order> Orders { get; set; }
    }
}