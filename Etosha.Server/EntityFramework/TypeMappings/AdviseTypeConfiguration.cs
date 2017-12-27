using Etosha.Server.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etosha.Server.EntityFramework.TypeMappings
{
	internal class AdviseTypeConfiguration : BaseTypeConfiguration<Advise>
	{
		public override void Configure(EntityTypeBuilder<Advise> builder)
		{
			base.Configure(builder);

			builder.Property(b => b.CreatedBy).IsRequired().HasMaxLength(256);
		}
	}
}
