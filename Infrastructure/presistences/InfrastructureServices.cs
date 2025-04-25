using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using presistences.Data;
using presistences.Identity;
using presistences.Repository;
using Services;
using ServicesAbstractions;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presistences
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configur)
        {
            if (configur == null)
            {
                throw new ArgumentNullException(nameof(configur), "Configuration is null");
            }

            services.AddDbContext<StoreDbContext>(option =>
                option.UseSqlServer(configur.GetConnectionString("DefaultConnection")));

            services.AddDbContext<StoreIdentityDbContext>(option =>
               option.UseSqlServer(configur.GetConnectionString("IdentityConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IServiceBasket, BasketService>();
            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
                return ConnectionMultiplexer.Connect(configur.GetConnectionString("Redis"));
            });

            return services;
        }
    }

}
