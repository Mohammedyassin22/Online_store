using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DeliveryMethod:BaseEntity<int>
    {
        public DeliveryMethod() { }
        public DeliveryMethod(string shortName, string description, decimal price, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            cost = price;
            DeliveryTime = deliveryTime;
        }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public decimal cost { get; set; }
        public string DeliveryTime { get; set; }

    }
}
