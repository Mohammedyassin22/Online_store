using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ProductOrderItems
    {
        private ProductOrderItems() { }

        public ProductOrderItems(int productId, string productName, string pictureUrl)
        {
            productId = productId;
            productName = productName;
            pictureUrl = pictureUrl;
        }
        public int productId { get; set; }
        public string productName { get; set; }
        public string pictureUrl { get; set; }
    }
}
