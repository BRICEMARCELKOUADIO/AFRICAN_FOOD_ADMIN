using AFRICAN_FOOD.Models;
using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFRICAN_FOOD.Services.Data
{
    public class BaseService
    {
        protected IBlobCache Cache;

        public BaseService(IBlobCache cache)
        {
            Cache = cache ?? BlobCache.LocalMachine;
        }

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                T t = await Cache.GetObject<T>(cacheName);
                return t;
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        public void InvalidateCache()
        {
            Cache.InvalidateAllObjects<Pie>();
        }
    }
}
