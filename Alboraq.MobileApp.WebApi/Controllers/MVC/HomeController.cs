using System.Web.Mvc;

namespace Alboraq.MobileApp.WebApi.Controllers.MVC
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
