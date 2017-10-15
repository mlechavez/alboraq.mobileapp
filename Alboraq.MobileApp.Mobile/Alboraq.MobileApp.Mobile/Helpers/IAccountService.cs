using Alboraq.MobileApp.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Helpers
{
    public interface IAccountService
    {
        Task<HttpResponseMessage> LoginAsync(string username, string password);
        Task<HttpResponseMessage> RegisterAsync(RegisterModel registerModel);
        Task<AccountInfoModel> GetAccountInfoAsync(string email, string token);
    }
}
