using System.ComponentModel.DataAnnotations;

namespace BookLibraryAPI.Models;

public class BookBaseDTO
{
  public string? Title { get; set; }
  public string? ISBN { get; set; }
  public DateTime? PublishedAt { get; set; }
  public int? AuthorId { get; set; }
  public int? CategoryId { get; set; }
  public bool? IsRead { get; set; } = false;
}

public class BookSetDTO : BookBaseDTO
{
  [Required]
  public new required string Title { get; set; }

  [Required]
  public new required DateTime PublishedAt { get; set; }

  [Required]
  public new required int AuthorId { get; set; }

  [Required]
  public new bool IsRead { get; set; } = false;
}

public class Book : BookSetDTO, IEntity
{
  [Required(ErrorMessage = "Id is required")]
  public int Id { get; set; }

  [Required(ErrorMessage = "Created timestamp is required")]
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
}
