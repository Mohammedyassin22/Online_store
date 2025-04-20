using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithCount:BaseSpectifications<Product,int>
    {
        public ProductWithCount(ProductSpecificationParameter specparams) : base(p =>
        (string.IsNullOrEmpty(specparams.Serach)||p.Name.ToLower().Contains(specparams.Serach.ToLower()))&&
            (!specparams.Brandid.HasValue || p.BrandId == specparams.Brandid) &&
        (!specparams.Typeid.HasValue || p.TypeId == specparams.Typeid)
            )
        {

        }
    }
}
