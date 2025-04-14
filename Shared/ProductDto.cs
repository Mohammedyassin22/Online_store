using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
    }
}
