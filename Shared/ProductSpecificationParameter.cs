using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationParameter
    {
        public int? Brandid {  get; set; }
        public int? Typeid {  get; set; }
        public string? Sort { get; set; }
        public string? Serach { get; set; }
        private int _pageindex;
        private int _pagesize;

        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }

        public int PageIndex
        {
            get { return _pageindex; }
            set { _pageindex = value; }
        }

    }
}
