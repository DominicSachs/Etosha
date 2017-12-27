using Etosha.Server.Common.Models;
using Etosha.Server.Entities;
using Etosha.Server.EntityFramework.TypeMappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Etosha.Server.EntityFramework
{
	internal class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
	{
		public AppDbContext() { }

		public AppDbContext(DbContextOptions options) : base(options) { }

		public DbSet<Advise> Advises { get; set; }

		public override int SaveChanges()
		{
			var addedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);

			foreach (var item in addedEntries)
			{
				item.Property(nameof(BaseEntity.CreationDate)).CurrentValue = DateTime.Now;
				item.Property(nameof(BaseEntity.ModifiedDate)).CurrentValue = DateTime.Now;
			}

			var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

			foreach (var item in modifiedEntries)
			{
				item.Property(nameof(BaseEntity.ModifiedDate)).CurrentValue = DateTime.Now;
			}

			return base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.ApplyConfiguration(new AdviseTypeConfiguration());
			builder.ApplyConfiguration(new AppUserTypeConfiguration());
			builder.ApplyConfiguration(new AppRoleTypeConfiguration());
		}
	}
}

// dotnet ef migrations add InitialCreate --startup-project ../Etosha.Web.Api/
// dotnet ef database update --startup-project ../Etosha.Web.Api/