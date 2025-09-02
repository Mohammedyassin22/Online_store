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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.AddressShipping, a =>
            {
                a.WithOwner();
            });
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.orderPayment).HasConversion(
                               o => o.ToString(),o => (OrderPayment)Enum.Parse(typeof(OrderPayment), o));
            builder.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");

        }
    }
}
