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
    public class CommentConfiguration : IEntityTypeConfiguration<CommentContract>
    {
        public void Configure(EntityTypeBuilder<CommentContract> builder)
        {
            builder.Property(x=>x.Comment).IsRequired().HasMaxLength(256);
            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => x.SharedId);
        }
    }
}
