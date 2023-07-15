using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;

namespace Business.Layer.Configurations
{
    public class SharedConfiguration : IEntityTypeConfiguration<SharedContract>
    {
        public void Configure(EntityTypeBuilder<SharedContract> builder)
        {
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(256);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Path).IsRequired(false).HasMaxLength(250);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.CreatedUserId);
         }
    }
}
