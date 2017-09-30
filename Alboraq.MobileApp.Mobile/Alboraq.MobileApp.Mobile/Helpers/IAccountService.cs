using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Helpers
{
    public interface IAccountService
    {
        Task<bool> Login(string username, string password, string grantType);
        Task<bool> Register(string username, string password, string confirmPassword, string plateNo, string mobileNo);  
    }
}
