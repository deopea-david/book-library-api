using BookLibraryAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookLibraryAPI.Infra.Data.Configurations;

public class AuthorItemConfiguration : EntityConfiguration<AuthorItem>
{
  public new void Configure(EntityTypeBuilder<AuthorItem> builder)
  {
    base.Configure(builder);
  }
}
