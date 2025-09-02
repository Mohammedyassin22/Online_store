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
using DeliveryMethod = Domain.Models.DeliveryMethod;

namespace Services
{
    public class OrderServices(IMapper mapper,IUnitOfWork unitOfWork,IBasketRepository basketRepository) : IOrederServices
    {
       
        public async Task<OrderRequestDto> CreateOrderAsync(string userEmail, OrderRequestDto orderRequest)
        {
            var address = mapper.Map<Address>(orderRequest.ShipToAddress);
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);
            if (basket == null) throw new BasketNotFoundException(orderRequest.BasketId);
            var orderitems= new List<OrderItem>();
            foreach (var item in basket.items)
            {
                var product = await unitOfWork.GetRepository<Product,int>().GetAsync(item.Id);
                if (product == null) throw new ProductNotFoundException(item.Id);
                var itemOrdered = new OrderItem(new ProductOrderItems(product.Id, product.Name, product.PictureUrl),product.Price, item.Quantity);
                orderitems.Add(itemOrdered);
            }
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod,int>().GetAsync(orderRequest.DeliveryMethodId);
            if (deliveryMethod == null) throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);
            var subtotal = orderitems.Sum(item => item.Price * item.Quantity);
            var order = new Order(orderitems, userEmail, address, deliveryMethod, subtotal, orderitems);
            var count = await unitOfWork.SaveChangesAsync() ;
            if (count == 0) throw new OrderCreateBadRequstException("Failed to create order");
            var result = mapper.Map<OrderRequestDto>(order);
            return result;
        }

        public Task<OrderResultDto> GetOrderByIdAsync(Guid Id)
        {
            var spec=new Specifications.OrderSpecifications(Id);
            var order= unitOfWork.GetRepository<Order,Guid>().GetAsync(spec);
            if(order is null) throw new OrderNotFoundException(Id);
            var result=mapper.Map<OrderResultDto>(order);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<OrderResultDto>> GetOrderEmail(string userEmail)
        {
            var spec = new Specifications.OrderSpecifications(userEmail);
            var orders = unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);
            var result = mapper.Map<IEnumerable<OrderResultDto>>(orders);
            return Task.FromResult(result);
        }

        Task<IEnumerable<Shared.DeliveryMethod>> IOrederServices.GetDeliveryMethodsAsync()
        {
            var deliveryMethods = unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsunc();
            var result = mapper.Map<IEnumerable<Shared.DeliveryMethod>>(deliveryMethods);
            return Task.FromResult(result);
        }
    }
}
 