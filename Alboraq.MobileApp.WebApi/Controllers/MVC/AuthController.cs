using Alboraq.MobileApp.WebApi.Models;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Alboraq.MobileApp.WebApi.Controllers.MVC
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ApplicationUserManager _userManager;

        public AuthController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET: Auth
        public ActionResult Signin()
        {
            return View();
        }

        public ActionResult Signout()
        {
            var context = Request.GetOwinContext();
            context.Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("signin", "auth");
        }

        public ActionResult ConfirmPasswordReset(string key, string id)
        {
            var viewModel = new ConfirmPasswordBindingModel
            {
                Key = key,
                UserID = id
            };
            return View("_confirmPasswordResetPartialView", viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmPasswordReset(ConfirmPasswordBindingModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("_confirmPasswordResetPartialView", viewModel);
            }

            var result = await _userManager.ResetPasswordAsync(viewModel.UserID, viewModel.Key, viewModel.NewPassword);

            if (result.Succeeded)
            {
                return RedirectToActionPermanent("index", "home");
            }            

            return View("_confirmPasswordResetPartialView", viewModel);
        }
    }
}