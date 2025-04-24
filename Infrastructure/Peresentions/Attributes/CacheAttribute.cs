using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresentions.Attributes
{
    public class CacheAttribute(int durationInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheservice = context.HttpContext.RequestServices.GetRequiredService<IServiceProduct>().cahceService;
            var cachekey = GenerateCache(context.HttpContext.Request);
            var result=await cacheservice.GetCacheValueAsync(cachekey);
            if(!string.IsNullOrEmpty(result))
            {
                context.Result = new ContentResult()
                {
                    ContentType="application/json",
                    StatusCode=StatusCodes.Status200OK,
                    Content = result
                };
                return;
            }
        }
        private string GenerateCache(HttpRequest request)
        {
            var key=new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(x=>x.Key)) {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
