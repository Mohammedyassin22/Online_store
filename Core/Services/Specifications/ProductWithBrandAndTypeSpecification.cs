using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandAndTypeSpecification:BaseSpectifications<Product,int>
    {
        private void ApplyIncludes()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
        public ProductWithBrandAndTypeSpecification(int? brandid, int? typeid) : base(p=>
            (!brandid.HasValue || p.BrandId==brandid)&&
        (!typeid.HasValue || p.TypeId == brandid)
            )
        {
            ApplyIncludes();
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
    }
}
