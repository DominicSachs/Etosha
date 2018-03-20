using Etosha.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etosha.Server.EntityFramework.TypeMappings
{
	internal class AppRoleTypeConfiguration : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
            builder.HasKey(p => p.Id);
			builder.Property(p => p.Name).IsRequired();
			builder.Property(p => p.NormalizedName).IsRequired();
		}
	}
}
