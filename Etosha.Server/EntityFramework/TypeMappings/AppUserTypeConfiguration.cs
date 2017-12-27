using Etosha.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etosha.Server.EntityFramework.TypeMappings
{
	internal class AppUserTypeConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			builder.Property(p => p.FirstName).IsRequired().HasMaxLength(256);
			builder.Property(p => p.FirstName).IsRequired().HasMaxLength(256);
			builder.Property(p => p.Email).IsRequired();
			builder.Property(p => p.NormalizedEmail).IsRequired();
			builder.Property(p => p.UserName).IsRequired();
			builder.Property(p => p.NormalizedUserName).IsRequired();
		}
	}
}
