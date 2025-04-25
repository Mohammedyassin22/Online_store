using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface IServiceProduct
    {
        IProductServices Services { get; }
        IServiceBasket basketServices { get; }
        ICahceService cahceService { get; }
        IAuthService AuthService { get; }
    }
}
