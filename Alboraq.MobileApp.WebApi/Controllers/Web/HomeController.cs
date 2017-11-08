using System.Web.Mvc;

namespace Alboraq.MobileApp.WebApi.Controllers.Web
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
