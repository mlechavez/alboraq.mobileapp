using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Helpers
{
    public interface ICacheService
    {
        Task<T> GetObject<T>(string key);
        Task InsertObject<T>(string key, T Value);
        Task RemoveObject(string key);
        Task<IEnumerable<string>> GetAllKeys();

    }
}
