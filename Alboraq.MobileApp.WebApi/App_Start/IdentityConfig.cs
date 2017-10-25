using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Alboraq.MobileApp.WebApi.Models;
using System;
using System.Net.Mail;
using Alboraq.MobileApp.EF;

namespace Alboraq.MobileApp.WebApi
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
            
            // Configure validation logic for usernames
            UserValidator = new UserValidator<ApplicationUser>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Configure email Service
            AppEmailService = new AppEmailService();

            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
        }        

        public IAppEmailService AppEmailService { get; set; }

        public async Task SendAppEmailAsync(string subject, string body, string to)
        {
            if (AppEmailService == null) throw new NotImplementedException("AppEmailService has not been implemented.");

            var message = new IdentityMessage { Subject = subject, Body = body, Destination = to };

            await AppEmailService.SendAsync(message);
        }
    }


    public interface IAppEmailService
    {
        Task SendAsync(IdentityMessage message);
    }

    public class AppEmailService : IAppEmailService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("kyocera.km3060@boraq-porsche.com.qa")                
            };
            mailMessage.To.Add(new MailAddress(message.Destination));
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Body;
            mailMessage.IsBodyHtml = true;

            using (var client = new SmtpClient("192.168.6.9", 25))
            {
                client.Credentials = new System.Net.NetworkCredential("kyocera.km3060@boraq-porsche.com.qa", "kyocera123");
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
