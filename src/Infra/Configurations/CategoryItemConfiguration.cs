using BookLibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPI.Infra.Data.Configurations;

public class CategoryItemConfiguration : EntityConfiguration<CategoryItem>
{
  public new void Configure(EntityTypeBuilder<CategoryItem> builder)
  {
    base.Configure(builder);
  }
}
