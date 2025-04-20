using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PaginationResponse<TEntitiy>
    {
        public PaginationResponse(int pageindex, int pagesize, int totalcount,IEnumerable<TEntitiy>data)
        {
            Pageindex = pageindex;
            Pagesize = pagesize;
            Totalcount = totalcount;
            Data = data;
        }
        public int Pageindex { get; set; }
        public int Pagesize { get; set; }
        public int Totalcount { get; set; }
        public IEnumerable<TEntitiy> Data {  get; set; }
    }
}
