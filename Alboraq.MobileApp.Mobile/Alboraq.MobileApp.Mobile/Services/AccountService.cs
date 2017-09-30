using Alboraq.MobileApp.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Services
{
    public class AccountService: IAccountService
    {
        public async Task<bool> Login(string username, string password, string grantType)
        {
            return await Task.FromResult(true);
        }

        public Task<bool> Register(string username, string password, string confirmPassword, string plateNo, string mobileNo)
        {
            throw new NotImplementedException();
        }
    }
}
