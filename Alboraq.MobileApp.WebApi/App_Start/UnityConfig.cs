using Alboraq.MobileApp.Core;
using Alboraq.MobileApp.EF;
using Alboraq.MobileApp.WebApi.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Alboraq.MobileApp.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			      var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>(new InjectionConstructor("DefaultConnection"));
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<RoleStore<IdentityRole>>(new InjectionConstructor(new Models.ApplicationDbContext()));
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<AppointmentController>(new InjectionConstructor(new UnitOfWork("DefaultConnection")));            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}