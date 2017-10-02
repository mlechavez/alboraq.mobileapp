using Akavache;
using Alboraq.MobileApp.Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alboraq.MobileApp.Mobile.Services
{
    public class CacheService : ICacheService
    {
        public CacheService()
        {
            BlobCache.ApplicationName = "AlboraqApp";
        }

        public async Task<IEnumerable<string>> GetAllKeys()
        {
            return await BlobCache.LocalMachine.GetAllKeys();
        }

        public async Task<T> GetObject<T>(string key)
        {
            try
            {
                return await BlobCache.LocalMachine.GetObject<T>(key);
            }
            catch (KeyNotFoundException)
            {

                return default(T);
            }
        }

        public async Task InsertObject<T>(string key, T value)
        {
            await BlobCache.LocalMachine.InsertObject<T>(key, value);
        }

        public async Task RemoveObject(string key)
        {
            await BlobCache.LocalMachine.Invalidate(key);            
        }
    }
}
