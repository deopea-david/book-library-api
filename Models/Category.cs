using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Models;

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

public class Category : CategorySetDTO, IEntity
{
  [Required(ErrorMessage = "Id is required")]
  public int Id { get; set; }

  [Required(ErrorMessage = "Created timestamp is required")]
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}
