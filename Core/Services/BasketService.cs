using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using ServicesAbstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService : IServiceBasket
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        // Constructor
        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        // Delete Basket
        public async Task<bool> DeleteBascketAsync(string id)
        {
            var flag = await basketRepository.DeleteBasketAsync(id);
            if (flag == false) throw new BasketDeleteBadRequest();
            return flag;
        }

        // Get Basket
        public async Task<BasketDto?> GetBasketAsync(string id)
        {
            var basket = await basketRepository.GetBasketAsync(id);
            if (basket is null) throw new BasketNotFoundException(id);
            var result = mapper.Map<BasketDto>(basket);
            return result;
        }

        // Update Basket
        public async Task<BasketDto?> UpdateBasketAsync(BasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);
            basket = await basketRepository.UpdateBasketAsync(basket);
            if (basket is null) throw new BasketCreateOrUpdateBaadRequestException();
            var result = mapper.Map<BasketDto>(basket);
            return result;
        }
    }
}
