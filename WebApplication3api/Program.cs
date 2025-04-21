
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using OnlineStore.MiddleWares;
using presistences;
using presistences.Data;
using Services;
using ServicesAbstractions;
using System.Globalization;
using AssemplyReference = Services.AssemplyReference;

namespace WebApplication3api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //seeding
            builder.Services.AddScoped<IDbInitializer,DbInitializer>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductServices,ProductServices>();
            builder.Services.AddScoped<IServiceProduct, ServiceProduct>();

            builder.Services.AddAutoMapper(typeof(AssemplyReference).Assembly);
            

            var app = builder.Build();
            //sedding
            using var scope=app.Services.CreateScope();
            var dbInitializer=scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.Initializer();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
