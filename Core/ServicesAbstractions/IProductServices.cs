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
        Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecificationParameter specparams);
        Task<ProductDto>GetProductGetId(int productId);
        Task <IEnumerable<TypeDto>>GetAllTypesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandAsync();
    }
}
