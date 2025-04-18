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
        public ProductWithBrandAndTypeSpecification(int? brandid, int? typeid,string? sort) : base(p=>
            (!brandid.HasValue || p.BrandId==brandid)&&
        (!typeid.HasValue || p.TypeId == brandid)
            )
        {
            ApplyIncludes();
            ApplySorting(sort);
        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            ApplyIncludes();
        }
    }
}
