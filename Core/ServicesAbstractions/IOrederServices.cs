using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface IOrederServices
    {
        Task<OrderResultDto> GetOrderByIdAsync(Guid Id);
        Task<IEnumerable<OrderResultDto>>GetOrderEmail(string userEmail);
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
        Task<OrderRequestDto> CreateOrderAsync(string userEmail, OrderRequestDto orderRequest);
    }
}
