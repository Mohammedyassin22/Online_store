using Domain.Models;
using Shared;
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
        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch(sort.ToLower())
                {
                    case "namease":
                        AddOrderBy(p => p.Name);
                        break;
                    case "namedesc":
                        AddOrderByDesc(p => p.Name);
                        break;
                    case "pricease":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            }
        }
        public ProductWithBrandAndTypeSpecification(ProductSpecificationParameter specparams) : base(p=>
         (string.IsNullOrEmpty(specparams.Serach) || p.Name.ToLower().Contains(specparams.Serach.ToLower())) &&
            (!specparams.Brandid.HasValue || p.BrandId==specparams.Brandid)&&
        (!specparams.Typeid.HasValue || p.TypeId == specparams.Typeid)
            )
        {
            ApplyIncludes();
            ApplySorting(specparams.Sort);
            ApplyPgination(specparams.PageIndex,specparams.PageSize);
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
    }
}
