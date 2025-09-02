using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderItem:BaseEntity<int>
    {

        public ProductOrderItems ProductOrderItems { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        private OrderItem() { }
        public OrderItem(ProductOrderItems productOrderItems, decimal price, int quantity)
        {
            ProductOrderItems = productOrderItems;
            Price = price;
            Quantity = quantity;
        }
    }
}
