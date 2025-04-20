using Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? brandid, int? typeid,string? sort, int indexpage = 1, int pagesize = 5);
        Task<ProductDto>GetProductGetId(int productId);
        Task <IEnumerable<TypeDto>>GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandAsync();
    }
}
