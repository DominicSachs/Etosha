using Etosha.Server.Entities;
using Etosha.Server.EntityFramework.TypeMappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Etosha.Server.Tests")]

namespace Etosha.Server.EntityFramework
{
    internal class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public override int SaveChanges()
        {
            var addedEntries = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Added);

            foreach (var item in addedEntries)
            {
                item.Property(nameof(BaseEntity.CreationDate)).CurrentValue = DateTime.Now;
                item.Property(nameof(BaseEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }

            var modifiedEntries = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Modified);

            foreach (var item in modifiedEntries)
            {
                item.Property(nameof(BaseEntity.ModifiedDate)).CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AppUserTypeConfiguration());
            builder.ApplyConfiguration(new AppRoleTypeConfiguration());
        }
    }
}

// dotnet ef migrations add InitialCreate --startup-project ../Etosha.Web.Api/
// dotnet ef database update --startup-project ../Etosha.Web.Api/
