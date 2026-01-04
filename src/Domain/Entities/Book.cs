using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BookLibraryAPI.Domain.Common;

namespace BookLibraryAPI.Domain.Entities;

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

public class BookItem : BookSetDTO, IEntity
{
  [Required(ErrorMessage = "Id is required")]
  public int Id { get; set; }

  [Required(ErrorMessage = "Created timestamp is required")]
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }

  public AuthorItem? Author { get; set; }
  public CategoryItem? Category { get; set; }
}
