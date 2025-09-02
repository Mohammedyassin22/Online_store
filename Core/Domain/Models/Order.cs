using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order:BaseEntity<Guid>
    {
        public string usermail { get; set; }
        public string AddressShipping { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? Delivery { get; set; }
        public OrderPayment orderPayment { get; set; }=OrderPayment.pending;
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        private Order() { } // مهم لـ EF Core

        public Order(
            IReadOnlyList<OrderItem> orderItems,
            string buyerEmail,
            Address shipToAddress,
            DeliveryMethod deliveryMethod,
            decimal subtotal, ICollection<OrderItem> orderItem)
        {
            OrderItems = orderItem;
            usermail = buyerEmail;
            AddressShipping = AddressShipping;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
        }
    }
}
