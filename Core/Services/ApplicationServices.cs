using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AssemplyReference).Assembly);
            services.AddScoped<IServiceProduct, ServiceProduct>();
            return services;
        }
    }
}
