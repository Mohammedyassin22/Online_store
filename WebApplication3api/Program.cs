
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
            //  √ﬂœ „‰ √‰ «· ÂÌ∆… (configuration) ·Ì”  null ⁄‰œ  „—Ì—Â« ≈·Ï RegisterAllServer
            var builder = WebApplication.CreateBuilder(args);

            //  √ﬂœ „‰ √‰ «· ÂÌ∆…  Õ ÊÌ ⁄·Ï «·ﬁÌ„ «·’ÕÌÕ…
            builder.Services.RegisterAllServer(builder.Configuration);

            var app = builder.Build();

            // «· ÂÌ∆… Ê«·≈⁄œ«œ«  «·‰Â«∆Ì…
            await app.configurationmiddleware();

            app.Run();
        }

    }
}
