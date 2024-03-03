using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Setsis.Core.Models;

namespace Setsis.Infrastructure
{
    public class SetsisDbContext : IdentityDbContext<ApplicationUser>
    {
        public SetsisDbContext(DbContextOptions<SetsisDbContext> options) : base(options) { }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}
