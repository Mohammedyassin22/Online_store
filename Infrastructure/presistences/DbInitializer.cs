using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using presistences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace presistences
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        public DbInitializer(StoreDbContext context)
        {
            _context = context;
        }
        public async Task Initializer()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
                if (!_context.ProductTypes.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\presistences\Data\Seeding\types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!_context.ProductBrands.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\presistences\Data\Seeding\brands.json");
                    var types = JsonSerializer.Deserialize<List<ProductBrand>>(typesData);
                    if (types is not null && types.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }

                if (!_context.Products.Any())
                {
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\presistences\Data\Seeding\products.json");
                    var types = JsonSerializer.Deserialize<List<Product>>(typesData);
                    if (types is not null && types.Any())
                    {
                        await _context.Products.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                throw;//يوقف ال app
            }
        }
    }
}


