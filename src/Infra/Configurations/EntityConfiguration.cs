using BookLibraryAPI.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPI.Infra.Data.Configurations;

public class EntityConfiguration<T> : IEntityTypeConfiguration<T>
  where T : class, IEntity
{
  public void Configure(EntityTypeBuilder<T> builder)
  {
    builder.Property(t => t.Id).IsRequired().UseAutoincrement();
    builder.Property(t => t.CreatedAt).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("current_timestamp");
    builder.Property(t => t.UpdatedAt).ValueGeneratedOnUpdate();
  }
}
