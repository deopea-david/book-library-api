using System.ComponentModel.DataAnnotations;
using BookLibraryAPI.Domain.Common;

namespace BookLibraryAPI.Domain.Entities;

public class AuthorBaseDTO
{
  public string? Name { get; set; }

  [MinLength(5, ErrorMessage = "Bio must have at least {0} character when specified")]
  public string? Bio { get; set; }
}

public class AuthorSetDTO : AuthorBaseDTO
{
  [Required(ErrorMessage = "Name is required")]
  public new required string Name { get; set; }
}

public class AuthorItem : AuthorSetDTO, IEntity
{
  [Required(ErrorMessage = "Id is required")]
  public int Id { get; set; }

  [Required(ErrorMessage = "Created timestamp is required")]
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}
