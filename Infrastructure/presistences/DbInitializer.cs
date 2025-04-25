using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using presistences.Data;
using presistences.Identity;
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
        private readonly StoreIdentityDbContext _storeIdentity;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(StoreDbContext context,StoreIdentityDbContext storeIdentity,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _storeIdentity = storeIdentity;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task InitializerIdentityAsync()
        {
            if (_storeIdentity.Database.GetPendingMigrations().Any())
            {
                await _storeIdentity.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin"
                });

                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
            }

            if (!_userManager.Users.Any())
            {
                var superadminuser = new AppUser
                {
                    DisplayName = "superadmin",
                    Email = "superadmin@gmail.com",
                    UserName = "superadmin",
                    PhoneNumber = "0123456789"
                };

                var adminuser = new AppUser
                {
                    DisplayName = "admin",
                    Email = "admin@gmail.com",
                    UserName = "admin",
                    PhoneNumber = "0123456788"
                };

                await _userManager.CreateAsync(superadminuser, "P@ssw0rd");
                await _userManager.CreateAsync(adminuser, "P@ssword");

                await _userManager.AddToRoleAsync(superadminuser, "SuperAdmin");
                await _userManager.AddToRoleAsync(adminuser, "Admin");
            }
        }

    }
}


