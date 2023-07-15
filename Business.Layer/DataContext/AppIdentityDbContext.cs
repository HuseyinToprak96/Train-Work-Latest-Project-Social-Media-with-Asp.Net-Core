using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Types.Layer.Contracts;

namespace Business.Layer.DataContext
{
    public class AppIdentityDbContext:IdentityDbContext<AppUserContract,IdentityRole,string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):base(options)
        {
            
        }

        public DbSet<CommentContract> Comments { get; set; }
        public DbSet<SharedContract> Shareds { get; set; }
        public DbSet<SharedContract> SharedLikes { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<FollowContract> Follows { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseContract entityReferences)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReferences.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReferences.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseContract entityReferences)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReferences.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReferences).Property(x => x.CreatedDate).IsModified = false;
                                entityReferences.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }


            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
    }
}
