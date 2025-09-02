using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.MiddleWares;
using presistences;
using presistences.Data;
using presistences.Identity;
using Services;
using ServicesAbstractions;
using Shared;
using Shared.ErrorModels;
using System.Runtime.CompilerServices;
using System.Text;

namespace OnlineStore.Extantion
{
    public static class Register
    {
        public static IServiceCollection RegisterAllServer(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //seeding
            services.AddInfrastructure(configuration);

            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<StoreIdentityDbContext>();

            services.AddScoped<IProductServices, ProductServices>();

            services.AddApplicationServices(configuration);

            //token Authorization
            var jwtoption = configuration.GetSection("Jwtoptions").Get<Jwtoption>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer=true,
                    ValidateAudience=true,
                    ValidateIssuerSigningKey=true,
                    ValidateLifetime=true,

                    ValidIssuer=jwtoption.Issuer,
                    ValidAudience=jwtoption.Audience,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoption.SecretKey)),
                };
            });

            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(m => m.Value.Errors.Any())
                        .Select(m => new ValidationError
                        {
                            Field = m.Key,
                            Errors = m.Value.Errors.Select(error => error.ErrorMessage)
                        });

                    var response = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }

        public static async Task <WebApplication> configurationmiddleware(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.Initializer();
            await dbInitializer.InitializerIdentityAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAuthorization();


            app.MapControllers();
            return app;
        }
    }
}
