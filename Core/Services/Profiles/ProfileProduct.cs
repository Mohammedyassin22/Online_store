using AutoMapper;
using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class ProfileProduct:Profile
    {
        public ProfileProduct() {
            CreateMap<Product, ProductDto>()
                .ForMember(x=>x.BrandName, o=>o.MapFrom(s=>s.ProductBrand.Name))
                .ForMember(x=>x.TypeName ,z=>z.MapFrom(s=>s.ProductType.Name))
                ;
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
