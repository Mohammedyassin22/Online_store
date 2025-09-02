using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class OrderResultDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public string paymentsatauts { get; set; }
        public ICollection<OrderItemDto> OrderItems { get; set; }=new List<OrderItemDto>();
        public decimal total { get; set; }
        public decimal subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;
        public string PaymentIntentId { get; set; }=string.Empty;
    }
}
