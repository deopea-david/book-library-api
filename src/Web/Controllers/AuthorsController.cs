using System.ComponentModel.DataAnnotations;
using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Domain.Entities;
using BookLibraryAPI.Web.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController(IAuthorService authorService) : ControllerBase
{
  private BadRequestObjectResult? ValidateAuthor(AuthorSetDTO author)
  {
    if (string.IsNullOrWhiteSpace(author.Name))
      return BadRequest(new { message = "Name is required" });

    return null;
  }

  [HttpGet]
  public async Task<ActionResult<AuthorItem[]>> GetAuthors(
    [FromQuery] int? id,
    [FromQuery] string? name,
    [FromQuery] [MinInt(1)] int? page,
    [FromQuery] [Range(1, 100)] int? pageSize
  )
  {
    var authors = await authorService.GetMany(id, name, page, pageSize);
    if (!authors.Any())
      return NotFound(new { message = "No authors matching the criteria were found" });

    return Ok(new { authors });
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<ActionResult<AuthorItem>> GetAuthor(int id)
  {
    var author = await authorService.GetById(id);
    if (author == null)
      return NotFound(new { message = "Author not found" });

    return Ok(author);
  }

  [HttpPost]
  public async Task<ActionResult<AuthorItem>> CreateAuthor([FromBody] AuthorSetDTO author)
  {
    var validationRes = ValidateAuthor(author);
    if (validationRes != null)
      return validationRes;

    var res = await authorService.Create(new() { Bio = author.Bio, Name = author.Name });
    return CreatedAtAction(nameof(GetAuthor), new { id = res.Id }, res);
  }

  [HttpPut]
  [Route("{id}")]
  public async Task<ActionResult<AuthorItem>> UpdateAuthor(int id, [FromBody] AuthorSetDTO author)
  {
    var validationRes = ValidateAuthor(author);
    if (validationRes != null)
      return validationRes;

    var orig = await authorService.GetById(id);
    if (orig == null)
      return NotFound(new { message = "Author not found" });

    var res = await authorService.Update(author, orig);
    return Ok(res);
  }

  [HttpPatch]
  [Route("{id}")]
  public async Task<ActionResult<AuthorItem>> PatchAuthor(int id, [FromBody] AuthorBaseDTO author)
  {
    var orig = await authorService.GetById(id);
    if (orig == null)
      return NotFound(new { message = "Author not found" });

    var res = await authorService.Update(author, orig);
    return Ok(res);
  }

  [HttpDelete]
  [Route("{id}")]
  public async Task<ActionResult<int>> DeleteAuthor(int id)
  {
    var res = await authorService.GetById(id);
    if (res == null)
      return NotFound(new { message = "Author not found" });

    await authorService.Delete(res);
    return Ok(id);
  }
}
