using Alboraq.MobileApp.WebApi.Models.MVC.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web.Mvc;

namespace Alboraq.MobileApp.WebApi.Controllers.MVC
{

    public class AdminController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ApplicationUserManager userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        // GET: Admin
        public ActionResult Settings()
        {            
            var viewModel = new AdminSettingsViewModel
            {
                Users = _userManager.Users.Where(x=>x.Email.Contains("boraq-porsche.com.qa")).ToList(),
                Roles = _roleManager.Roles.ToList()
            };
            return View(viewModel);
        }

        public ActionResult NewUserPartialView()
        {            
            return PartialView("_NewUserPartialView");
        }

        public ActionResult EditUserPartialView(string userID)
        {
            var user = _userManager.FindById(userID);

            return PartialView("_EditUserPartialView", user);
        }
    }
}