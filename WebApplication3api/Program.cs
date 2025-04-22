
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Extantion;
using OnlineStore.MiddleWares;
using presistences;
using presistences.Data;
using Services;
using ServicesAbstractions;
using Shared.ErrorModels;
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

            builder.Services.RegisterAllServer(builder.Configuration);



            var app = builder.Build();
            //sedding
           await app.configurationmiddleware();

            app.Run();
        }
    }
}
