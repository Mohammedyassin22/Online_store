using Domain.Contracts;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CahceService(ICacheRepository cacheRepository) : ICahceService
    {
        public async Task<string?> GetCacheValueAsync(string key)
        {
            var value=await cacheRepository.GetAsync(key);
            return value==null ? null: value;
        }

        public async Task SetCahceValueAsync(string key, object value, TimeSpan duration)
        {
            await cacheRepository.SetAsync(key, value, duration);
        }
    }
}
