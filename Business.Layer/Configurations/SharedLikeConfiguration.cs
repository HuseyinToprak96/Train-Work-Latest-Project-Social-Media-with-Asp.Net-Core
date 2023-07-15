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
    public class SharedLikeConfiguration : IEntityTypeConfiguration<SharedLikeContract>
    {
        public void Configure(EntityTypeBuilder<SharedLikeContract> builder)
        {
            builder.HasIndex(x => x.SharedId);
            builder.HasIndex(x => x.UserId);
        }
    }
}
