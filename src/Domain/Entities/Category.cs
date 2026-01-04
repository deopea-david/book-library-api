using System.ComponentModel.DataAnnotations;
using BookLibraryAPI.Domain.Common;

namespace BookLibraryAPI.Domain.Entities;

public class CategoryBaseDTO
{
  public string? Name { get; set; }

  [MinLength(5, ErrorMessage = "Description must have at least {0} characters when specified")]
  public string? Description { get; set; }
}

public class CategorySetDTO : CategoryBaseDTO
{
  [Required]
  public new required string Name { get; set; }
}

public class CategoryItem : CategorySetDTO, IEntity
{
  [Required(ErrorMessage = "Id is required")]
  public int Id { get; set; }

  [Required(ErrorMessage = "Created timestamp is required")]
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}
