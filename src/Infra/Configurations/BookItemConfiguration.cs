using BookLibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPI.Infra.Data.Configurations;

public class EntityConfiguration : EntityConfiguration<BookItem>
{
  public new void Configure(EntityTypeBuilder<BookItem> builder)
  {
    base.Configure(builder);
    builder.Property(t => t.IsRead).IsRequired().HasDefaultValue(false);
  }
}
