using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Specifications;
using ServicesAbstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductServices(IUnitOfWork unitOfWork ,IMapper mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsunc();
            var result=mapper.Map<IEnumerable<BrandDto>>(brands);
            return result;  
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? brandid,int? typeid,string? sort)
        {
            var spec = new ProductWithBrandAndTypeSpecification( brandid,  typeid,sort);
            var products = await unitOfWork.GetRepository<Product, int>().GetAllAsunc();
            var result = mapper.Map<IEnumerable<ProductDto>>(products);
            return result;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types=await unitOfWork.GetRepository<ProductType,int>().GetAllAsunc();
            var result=mapper.Map<IEnumerable<TypeDto>>(types);
            return result;
        }

        public async Task<ProductDto?> GetProductGetId(int productId)
        {

            var spec = new ProductWithBrandAndTypeSpecification(productId);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(productId);
            if (product is null) return null;
            var result=mapper.Map<ProductDto>(product);
            return result;
        }
    }
}
