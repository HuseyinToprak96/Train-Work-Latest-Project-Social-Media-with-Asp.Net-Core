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
    internal class FollowConfiguration : IEntityTypeConfiguration<FollowContract>
    {
        public void Configure(EntityTypeBuilder<FollowContract> builder)
        {
            builder.HasOne(x=>x.Follow).WithMany(x=>x.Follows).HasForeignKey(x=>x.FollowId);
            builder.HasOne(x => x.Following).WithMany(x => x.Followings).HasForeignKey(x => x.FollowingId);
        }
    }
}
