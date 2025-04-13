using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presistences.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(8,2)");

            builder.HasOne(x=>x.ProductBrand ).WithMany().HasForeignKey(x => x.BrandId);
            builder.HasOne(x => x.ProductType).WithMany().HasForeignKey(x => x.TypeId);
        }
    }
}
