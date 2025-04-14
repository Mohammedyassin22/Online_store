
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using presistences;
using presistences.Data;

namespace WebApplication3api
{
    public class Program
    {
        public static async void Main(string[] args)
        {
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
