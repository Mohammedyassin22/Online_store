using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.paymentsatauts, opt => opt.MapFrom(src => src.PaymentIntentId.ToString()))
                .ForMember(dest => dest.total, opt => opt.MapFrom(src => src.Subtotal+src.DeliveryMethod.cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductOrderItems.productId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductOrderItems.productName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.ProductOrderItems.pictureUrl));
        }
        
    }
}
