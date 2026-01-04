using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
  private BadRequestObjectResult? ValidateCategory(CategorySetDTO category)
  {
    if (string.IsNullOrWhiteSpace(category.Name))
      return BadRequest(new { message = "Name is required" });

    return null;
  }

  [HttpGet]
  public async Task<ActionResult<CategoryItem>> GetCategories([FromQuery] int? id, [FromQuery] string? name)
  {
    var categories = await categoryService.GetMany(id, name);
    if (!categories.Any())
      return NotFound(new { message = "No categories matching the criteria were found" });

    return Ok(new { categories });
  }

  [HttpGet]
  [Route("{id}")]
  public async Task<ActionResult<CategoryItem>> GetCategory(int id)
  {
    var category = await categoryService.GetById(id);
    if (category == null)
      return NotFound(new { message = "Category not found" });

    return Ok(category);
  }

  [HttpPost]
  public async Task<ActionResult<CategoryItem>> CreateCategory([FromBody] CategorySetDTO category)
  {
    var validationRes = ValidateCategory(category);
    if (validationRes != null)
      return validationRes;

    var res = await categoryService.Create(new() { Name = category.Name, Description = category.Description });
    return CreatedAtAction(nameof(GetCategory), new { id = res.Id }, res);
  }

  [HttpPut]
  [Route("{id}")]
  public async Task<ActionResult<CategoryItem>> UpdateCategory(int id, [FromBody] CategorySetDTO category)
  {
    var validationRes = ValidateCategory(category);
    if (validationRes != null)
      return validationRes;

    var orig = await categoryService.GetById(id);
    if (orig == null)
      return NotFound(new { message = "Category not found" });

    var res = await categoryService.Update(category, orig);
    return Ok(res);
  }

  [HttpPatch]
  [Route("{id}")]
  public async Task<ActionResult<CategoryItem>> PatchCategory(int id, [FromBody] CategoryBaseDTO category)
  {
    var orig = await categoryService.GetById(id);
    if (orig == null)
      return NotFound(new { message = "Category not found" });

    var res = await categoryService.Update(category, orig);
    return Ok(res);
  }

  [HttpDelete]
  [Route("{id}")]
  public async Task<ActionResult<CategoryItem>> DeleteCategory(int id)
  {
    var res = await categoryService.GetById(id);
    if (res == null)
      return NotFound(new { message = "Category not found" });

    await categoryService.Delete(res);
    return Ok(id);
  }
}
