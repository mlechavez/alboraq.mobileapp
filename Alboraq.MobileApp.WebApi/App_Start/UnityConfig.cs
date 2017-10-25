using Alboraq.MobileApp.Core;
using Alboraq.MobileApp.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;

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
            container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(), new InjectionConstructor("DefaultConnection"));

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole>>(new HierarchicalLifetimeManager(), new InjectionConstructor(new AlboraqAppContext()));

            container.RegisterType<ApplicationUserManager>(new HierarchicalLifetimeManager());
            container.RegisterType<DbContext, AlboraqAppContext>(new HierarchicalLifetimeManager(), new InjectionConstructor("DefaultConnection"));
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());

            container.RegisterType<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
            container.RegisterType<ISecureDataFormat<AuthenticationTicket>, TicketDataFormat>();
            container.RegisterType<IDataSerializer<AuthenticationTicket>, TicketSerializer>();
            container.RegisterType<IDataProtector>(new InjectionFactory(c => new DpapiDataProtectionProvider().Create("ASP.Net Identity")));
            
            //web api
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            //mvc
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
        }
    }
}