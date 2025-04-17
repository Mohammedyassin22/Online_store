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
    public class ProfileProduct : Profile
    {
        public ProfileProduct()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, o => o.MapFrom<PictureUrlReasor>());

            // Map ProductBrand → BrandDto
            CreateMap<ProductBrand, BrandDto>()
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Name));

            // Map ProductType → TypeDto
            CreateMap<ProductType, TypeDto>()
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
