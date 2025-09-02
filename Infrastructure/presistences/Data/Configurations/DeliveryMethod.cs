using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presistences.Data.Configurations
{
    public class DeliveryMethod: IEntityTypeConfiguration<Domain.Models.DeliveryMethod>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Models.DeliveryMethod> builder)
        {
            builder.Property(d => d.cost)
                .HasColumnType("decimal(18,2)");
        }
    }
    
}
