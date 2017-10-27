using Alboraq.MobileApp.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Alboraq.MobileApp.WebApi.Models.MVC.Admin
{
    public class AdminSettingsViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}