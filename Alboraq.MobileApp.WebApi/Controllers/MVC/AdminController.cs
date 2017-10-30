using Alboraq.MobileApp.WebApi.Models;
using Alboraq.MobileApp.WebApi.Models.MVC.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;
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

            var viewModel = new UpdateBindingModel { UserID = user.Id, Name = user.Name, PhoneNumber = user.PhoneNumber };

            return PartialView("_EditUserPartialView", viewModel);
        }

        [HttpPost]
        public ActionResult UpdateUser(UpdateBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { modelState = ModelState });
            }

            try
            {
                var user = _userManager.FindById(model.UserID);
                if (user == null)
                {
                    return Json(new { message = "user cannot be found!" });
                }

                user.Name = model.Name;
                user.PhoneNumber = model.PhoneNumber;
                var result = _userManager.Update(user);

                if (result.Succeeded)
                {
                    return Json(new { isSuccess = true, message = $"{user.Name } has been updated!" });
                }
                return Json(new { isSuccess = false, errors = result.Errors });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }
            return Json(new { exception = ModelState });
        }

        public ActionResult ResetPasswordPartialView(string email)
        {
            var user = _userManager.FindByEmail(email);

            var viewModel = new ResetPasswordBindingModel { Email = user.Email };

            return PartialView("_ResetPasswordPartialView", viewModel);
        }

        public async Task<ActionResult> ResetPassword(ResetPasswordBindingModel model)
        {
            var user = _userManager.FindByEmail(model.Email);

            if (user == null)
            {
                return Json(new { message = "We cannot retrieve the information about the user. Please try again later." });
            }
            var key = _userManager.GeneratePasswordResetToken(user.Id);

            var url = $"{Request.Url.Scheme}://{Request.Url.Authority}/{Url.Action("confirmpasswordreset", "auth", new { key = key, id = user.Id })}";

            try
            {
                await _userManager.SendAppEmailAsync("Password reset", $"Please click the link to confirm your password reset { url }", user.Email);
                return Json(new { message = "The link has been sent check the email." });
            }
            catch (Exception)
            {
                               
            }
            return Json(new { message = "Something went wrong while sending email from the server" });
        }

        public ActionResult DeleteUserPartialView(string email)
        {
            ViewBag.Email = email;

            return PartialView("_DeleteUserPartialView");
        }
    }
}