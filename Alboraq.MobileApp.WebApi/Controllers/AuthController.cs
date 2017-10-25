using System.Web;
using System.Web.Mvc;

namespace Alboraq.MobileApp.WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
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
    }
}