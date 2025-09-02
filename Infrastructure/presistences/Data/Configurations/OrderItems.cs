using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presistences.Data.Configurations
{
    public class OrderItems : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(o => o.ProductOrderItems, a =>
            {
                a.WithOwner();
            });
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
