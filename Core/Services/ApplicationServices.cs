using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServices                 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(AssemplyReference).Assembly);
            services.AddScoped<IServiceProduct, ServiceProduct>();
            services.Configure<Jwtoption>(configuration.GetSection("Jwtoptions"));
            return services;
        }
    }
}
