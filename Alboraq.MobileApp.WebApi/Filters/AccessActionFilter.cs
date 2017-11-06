using System.Web.Mvc;

namespace Alboraq.MobileApp.WebApi.Filters
{
    public class AccessActionFilter : ActionFilterAttribute
    {
        public string RoleName { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(!filterContext.HttpContext.User.IsInRole(RoleName))
            {
                filterContext.Result = new ViewResult { ViewName = "_UnAuthorized" };
            }
        }
    }
}