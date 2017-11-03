using Alboraq.MobileApp.WebApi.Models;
using Alboraq.MobileApp.WebApi.Models.MVC.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
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
                Users = _userManager.Users.Where(x => x.Email.Contains("boraq-porsche.com.qa")).ToList(),
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
        public async Task<ActionResult> UpdateUser(UpdateBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { modelState = ModelState });
            }

            try
            {
                var user = await _userManager.FindByIdAsync(model.UserID);

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
                return Json(new { isSuccess = false, errors = GetErrorResult(result) });
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

        public async Task<ActionResult> DeleteUser(string email)
        {
            var user = _userManager.FindByEmail(email);

            if (user == null)
            {
                return Json(new { isSuccess = false, message = "The user cannot be found! Maybe it's been deleted." });
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Json(new { isSuccess = true, message = $"{ user.Email} has been deleted."});
            }
            return Json(new { isSuccess = false, errors = result.Errors });
        }

        public ActionResult NewRolePartialView()
        {
            return PartialView("_NewRolePartialView");
        }

        public async Task<ActionResult> AddRole(AddRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { isSuccess = false, message = "Please enter a role name" });
            }

            var result = await _roleManager.CreateAsync(new IdentityRole { Name = model.RoleName });

            if (result.Succeeded)
            {
                return Json(new { isSuccess = true, message = $"Role { model.RoleName} has been added!" });
            }
            else
            {
                return Json(new { isSuccess = false, errors = GetErrorResult(result) });
            }               
        }

        public async Task<ActionResult> EditRolePartialView(string roleID)
        {
            var role = await _roleManager.FindByIdAsync(roleID);
            var bindingModel = new UpdateRoleBindingModel { RoleID = role.Id, RoleName = role.Name };
            return PartialView("_EditRolePartialView", bindingModel);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateRole(UpdateRoleBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { errors = new List<string> { "Role name cannot be empty" } });
            }

            IdentityResult result = null;
            var role = await _roleManager.FindByNameAsync(model.RoleName);

            if (role != null)
            {
                if (role.Id != model.RoleID)
                {
                    return Json(new { isSuccess = false, errors = new List<string> { "Name already taken" } });
                }

                if (role.Name.ToLower() == model.RoleName.ToLower())
                {
                    //there's no need to call db just return with success;s
                    return Json(new { isSuccess = true, message = "Role has been updated" });
                }
            }

            role = await _roleManager.FindByIdAsync(model.RoleID);            

            role.Name = model.RoleName.ToLower();

            result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return Json(new { isSuccess = true, message = "Role has been updated!" });
            }
            return Json(new { isSuccess = false, errors = GetErrorResult(result) });
        }

        public ActionResult AddUserToRolePartialView()
        {                        
            return PartialView("AddUserToRolePartialView");
        }

        #region Helpers
        private List<string> GetErrorResult(IdentityResult result)
        {            
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                var errorList = (from errors in ModelState.Values
                        from error in errors.Errors
                        select error.ErrorMessage).ToList();
                return errorList;
            }

            return null;
        }
        #endregion
    }
}