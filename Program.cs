using BookLibraryAPI.Data;
using BookLibraryAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SQLiteContext>();

// Register services with the DI container
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApiDocument();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  // Add OpenAPI 3.0 document serving middleware
  // Available at: http://localhost:<port>/swagger/v1/swagger.json
  app.UseOpenApi();

  // Add web UIs to interact with the document
  // Available at: http://localhost:<port>/swagger
  app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
