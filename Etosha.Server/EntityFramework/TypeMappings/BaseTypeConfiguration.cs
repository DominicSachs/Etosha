using Etosha.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Etosha.Server.EntityFramework.TypeMappings
{
	internal class BaseTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.Property(b => b.CreatedBy).IsRequired().HasMaxLength(256);
			builder.Property(b => b.CreationDate).IsRequired();
			builder.Property(b => b.ModifiedBy).IsRequired().HasMaxLength(256);
			builder.Property(b => b.ModifiedDate).IsRequired();
		}
	}
}
