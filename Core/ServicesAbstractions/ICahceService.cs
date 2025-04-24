using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface ICahceService
    {
        Task SetCahceValueAsync(string key,object value,TimeSpan duration);
        Task <string?>GetCacheValueAsync(string key);
    }
}
