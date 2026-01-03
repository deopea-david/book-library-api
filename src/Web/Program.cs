var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructureServices();

// Register services with the DI container
builder.AddApplicationServices();

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
