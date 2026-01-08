using System.ComponentModel.DataAnnotations;
using BookLibraryAPI.Application.Common.Interfaces;
using BookLibraryAPI.Domain.Entities;
using BookLibraryAPI.Web.Validation;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryAPI.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IAuthorService authorService, IBookService bookService, ICategoryService categoryService)
  : ControllerBase
{
  private async Task<BadRequestObjectResult?> ValidateBook(BookSetDTO book)
  {
    if (string.IsNullOrWhiteSpace(book.Title))
      return BadRequest(new { message = "Title is required" });

    var author = await authorService.GetById(book.AuthorId);
    if (author == null)
    {
      return BadRequest(new { message = "Author is invalid" });
    }

    var category = book.CategoryId == null ? null : await categoryService.GetById((int)book.CategoryId);
    if (book.CategoryId != null && category == null)
    {
      return BadRequest(new { message = "Category is invalid" });
    }

    return null;
  }

  [HttpGet]
  public async Task<ActionResult<BookItem[]>> GetBooks(
    [FromQuery] int? authorId,
    [FromQuery] int? categoryId,
    [FromQuery] string? title,
    [FromQuery] DateTime? publishedAt,
    [FromQuery] string? isbn,
    [FromQuery] bool? isRead,
    [FromQuery] [MinInt(1)] int? page,
    [FromQuery] [Range(1, 100)] int? pageSize
  )
  {
    var books = await bookService.GetMany(authorId, categoryId, title, publishedAt, isbn, isRead, page, pageSize);
    if (!books.Any())
      return NotFound(new { message = "No books matching the criteria were found" });

    return Ok(new { books });
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<BookItem?>> GetBook(int id)
  {
    var book = await bookService.GetById(id);
    if (book == null)
      return NotFound(new { message = "Book not found" });

    return Ok(book);
  }

  [HttpPost]
  public async Task<ActionResult<BookItem>> CreateBook(BookSetDTO book)
  {
    var validationRes = await ValidateBook(book);
    if (validationRes != null)
      return validationRes;

    var existingBook = book.ISBN == null ? [] : await bookService.GetMany(isbn: book.ISBN);
    if (existingBook.Any())
      return Conflict(new { message = "Book already exists" });

    var res = await bookService.Create(
      new()
      {
        Title = book.Title,
        ISBN = book.ISBN,
        PublishedAt = book.PublishedAt,
        AuthorId = book.AuthorId,
        CategoryId = book.CategoryId,
        IsRead = book.IsRead,
      }
    );
    return CreatedAtAction(nameof(GetBook), new { id = res.Id }, res);
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<BookItem>> UpdateBook(int id, [FromBody] BookSetDTO book)
  {
    var validationRes = await ValidateBook(book);
    if (validationRes != null)
      return validationRes;

    var orig = await bookService.GetById(id);
    if (orig == null)
      return NotFound(new { message = "Book not found" });

    var res = await bookService.Update(book, orig);
    return Ok(res);
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult<BookItem>> PatchBook(int id, [FromBody] BookBaseDTO book)
  {
    var orig = await bookService.GetById(id);
    if (orig == null)
      return NotFound(new { message = "Book not found" });

    var res = await bookService.Update(book, orig);
    return Ok(res);
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult<string>> DeleteBook(int id)
  {
    var res = await bookService.GetById(id);
    if (res == null)
      return NotFound(new { message = "Book not found" });

    await bookService.Delete(res);
    return Ok(id);
  }
}
