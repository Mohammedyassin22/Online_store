using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServicesAbstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceProduct(IUnitOfWork unitOfWork,IMapper mapper,IOptions<Jwtoption> configuration,IBasketRepository basketRepository,UserManager<AppUser> userManager,ICacheRepository cacheRepository) : IServiceProduct
    {
        public IProductServices Services { get; }=new ProductServices(unitOfWork,mapper);

        public IServiceBasket basketServices { get; } = new BasketService(basketRepository,mapper);

        public ICahceService cahceService { get; } = new CahceService(cacheRepository);

        public IAuthService AuthService { get; } = new AuthService(userManager,configuration);

        public IOrederServices orderServices { get; }=new OrderServices( mapper,unitOfWork, basketRepository);
    }
}
