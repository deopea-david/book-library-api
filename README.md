# Book Library API

A RESTful API built with ASP.NET Core for managing a personal book library. This project demonstrates modern .NET practices and core C# concepts including Object-Oriented Programming (OOP), LINQ, async/await patterns, dependency injection, Entity Framework Core, DTOs, and clean architecture patterns.

## Educational Purpose

This project was designed to practice and demonstrate key C# and .NET concepts:

| Concept                  | How It's Applied                                                                    |
| ------------------------ | ----------------------------------------------------------------------------------- |
| **OOP**                  | Models (Book, Author, Category), inheritance, interfaces (`IEntity`, `IService<T>`) |
| **LINQ**                 | Filtering books by author, category, title, publication date, ISBN, and read status |
| **Async/Await**          | All API endpoints and service methods use async patterns                            |
| **Dependency Injection** | Services injected into controllers via constructor injection                        |
| **Interfaces**           | `IService<T>`, `IBookService`, `IAuthorService`, `ICategoryService` for abstraction |
| **Generic Programming**  | Generic base service (`DbService<T>`) for reusable CRUD operations                  |

## Features

- **CRUD Operations** for Books, Authors, and Categories
- **Advanced Filtering** - Query books by author, category, title, publication date, ISBN, and read status
- **Partial Updates** - Support for both PUT (full update) and PATCH (partial update) operations
- **Automatic Timestamps** - `CreatedAt` and `UpdatedAt` fields are automatically managed
- **Data Validation** - Comprehensive validation with clear error messages
- **OpenAPI/Swagger** - Interactive API documentation (available in development mode)
- **SQLite Database** - Lightweight, file-based database for easy setup

## Technology Stack

- **.NET 10.0** - Latest .NET framework
- **ASP.NET Core** - Web API framework
- **Entity Framework Core 10.0** - ORM for database operations
- **SQLite** - Embedded database
- **NSwag** - OpenAPI/Swagger generation
- **C# 12** - Modern C# features (primary constructors, required properties, file-scoped namespaces)

## Project Structure

```
BookLibraryAPI/
├── Controllers/          # API endpoints (RESTful controllers)
│   ├── AuthorsController.cs
│   ├── BooksController.cs
│   └── CategoriesController.cs
├── Data/                # Database context and configuration
│   └── SQLiteContext.cs
├── Events/              # Structured logging event IDs
│   └── LogEvents.cs
├── Models/              # Domain models, DTOs, and interfaces
│   ├── Author.cs        # Author entity with BaseDTO/SetDTO hierarchy
│   ├── Book.cs          # Book entity with BaseDTO/SetDTO hierarchy
│   ├── Category.cs      # Category entity with BaseDTO/SetDTO hierarchy
│   └── Entity.cs        # IEntity interface for common properties
├── Services/            # Business logic layer
│   ├── AuthorService.cs
│   ├── BookService.cs
│   ├── CategoryService.cs
│   ├── DbService.cs     # Generic base service with CRUD operations
│   ├── IAuthorService.cs
│   ├── IBookService.cs
│   ├── ICategoryService.cs
│   └── IService.cs      # Generic service interface
└── Program.cs           # Application entry point and DI configuration
```

## Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download) or later
- A code editor (Visual Studio, VS Code, Rider, etc.)

### Installation

1. Clone the repository:

```bash
git clone <repository-url>
cd BookLibraryAPI
```

2. Restore dependencies:

```bash
dotnet restore
```

3. Build the project:

```bash
dotnet build
```

4. Run the application:

```bash
dotnet run
```

The API will be available at `https://localhost:<port>` or `http://localhost:<port>`.

### Database

The SQLite database (`book-library.db`) will be automatically created on first run. Entity Framework Core will handle database creation and migrations. The database file is stored in the project root directory.

**Note**: The database uses Entity Framework Core migrations. If you need to recreate the database, delete the `book-library.db` file and restart the application.

## API Endpoints

### Books

| Method   | Endpoint          | Description                           |
| -------- | ----------------- | ------------------------------------- |
| `GET`    | `/api/books`      | Get all books (with optional filters) |
| `GET`    | `/api/books/{id}` | Get a specific book by ID             |
| `POST`   | `/api/books`      | Create a new book                     |
| `PUT`    | `/api/books/{id}` | Update a book (full update)           |
| `PATCH`  | `/api/books/{id}` | Partially update a book               |
| `DELETE` | `/api/books/{id}` | Delete a book                         |

**Query Parameters for GET `/api/books`:**

- `authorId` (int?) - Filter by author ID
- `categoryId` (int?) - Filter by category ID
- `title` (string?) - Filter by title (exact match)
- `publishedAt` (DateTime?) - Filter by publication date
- `isbn` (string?) - Filter by ISBN
- `isRead` (bool?) - Filter by read status

### Authors

| Method   | Endpoint            | Description                             |
| -------- | ------------------- | --------------------------------------- |
| `GET`    | `/api/authors`      | Get all authors (with optional filters) |
| `GET`    | `/api/authors/{id}` | Get a specific author by ID             |
| `POST`   | `/api/authors`      | Create a new author                     |
| `PUT`    | `/api/authors/{id}` | Update an author (full update)          |
| `PATCH`  | `/api/authors/{id}` | Partially update an author              |
| `DELETE` | `/api/authors/{id}` | Delete an author                        |

**Query Parameters for GET `/api/authors`:**

- `id` (int?) - Filter by author ID
- `name` (string?) - Filter by name (exact match)

### Categories

| Method   | Endpoint               | Description                                |
| -------- | ---------------------- | ------------------------------------------ |
| `GET`    | `/api/categories`      | Get all categories (with optional filters) |
| `GET`    | `/api/categories/{id}` | Get a specific category by ID              |
| `POST`   | `/api/categories`      | Create a new category                      |
| `PUT`    | `/api/categories/{id}` | Update a category (full update)            |
| `PATCH`  | `/api/categories/{id}` | Partially update a category                |
| `DELETE` | `/api/categories/{id}` | Delete a category                          |

**Query Parameters for GET `/api/categories`:**

- `id` (int?) - Filter by category ID
- `name` (string?) - Filter by name (exact match)

## Data Models

### Book

```json
{
  "id": 1,
  "title": "The Great Gatsby",
  "isbn": "978-0-7432-7356-5",
  "publishedAt": "1925-04-10T00:00:00",
  "authorId": 1,
  "categoryId": 2,
  "isRead": true,
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": "2024-01-20T14:45:00"
}
```

**Required fields for POST/PUT:**

- `title` (string)
- `publishedAt` (DateTime)
- `authorId` (int)
- `isRead` (bool)

**Optional fields:**

- `isbn` (string?)
- `categoryId` (int?)

### Author

```json
{
  "id": 1,
  "name": "F. Scott Fitzgerald",
  "bio": "American novelist and short story writer",
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": null
}
```

**Required fields for POST/PUT:**

- `name` (string)

**Optional fields:**

- `bio` (string?, minimum 5 characters if provided)

### Category

```json
{
  "id": 1,
  "name": "Fiction",
  "description": "Literary works of imagination",
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": null
}
```

**Required fields for POST/PUT:**

- `name` (string)

**Optional fields:**

- `description` (string?, minimum 5 characters if provided)

## Architecture

### DTO Pattern

The project uses a hierarchical DTO pattern to separate concerns and provide flexibility:

- **BaseDTO** - All properties optional (used for PATCH operations)
  - Example: `BookBaseDTO`, `AuthorBaseDTO`, `CategoryBaseDTO`
- **SetDTO** - Required properties marked with `[Required]` (used for POST/PUT operations)
  - Example: `BookSetDTO`, `AuthorSetDTO`, `CategorySetDTO`
- **Entity** - Full entity with Id and timestamps (database model)
  - Example: `Book`, `Author`, `Category`
  - All entities implement `IEntity` interface

This pattern allows:

- Clients to send only the fields they want to update (PATCH)
- Validation of required fields (POST/PUT)
- Automatic timestamp management (server-side only)
- Type safety while maintaining flexibility

### Service Layer

The service layer follows a generic pattern for code reuse:

- **Generic Base Service** (`DbService<T>`) - Provides common CRUD operations
  - `Create`, `GetById`, `GetMany`, `Update`, `Delete`
  - Virtual methods `OnCreate` and `OnUpdate` for customization
  - Uses Entity Framework Core for database operations
- **Specific Services** - Extend base service with domain-specific logic
  - `BookService` - Adds filtering by multiple criteria
  - `AuthorService` - Adds filtering by ID and name
  - `CategoryService` - Adds filtering by ID and name
- **Dependency Injection** - All services registered as scoped services in `Program.cs`
- **Interface-Based Design** - All services implement interfaces for testability

### Automatic Timestamps

The `CreatedAt` and `UpdatedAt` fields are managed through the `IEntity` interface:

- **CreatedAt** - Automatically set to `DateTime.Now` in `OnCreate` method
- **UpdatedAt** - Automatically set to `DateTime.Now` in `OnUpdate` method
- **Server-Side Only** - Cannot be set by client requests (protected via DTO pattern)
- **Always Included** - Timestamps are included in all API responses
- **Consistent** - All entities (`Book`, `Author`, `Category`) follow the same pattern

### Entity Framework Core

- **SQLite Provider** - Lightweight, file-based database
- **DbContext** - `SQLiteContext` manages database connections and entity sets
- **Code-First Approach** - Database schema defined by entity classes
- **Automatic Migrations** - Database created automatically on first run

## API Documentation

When running in **Development** mode, Swagger UI is available at:

- `http://localhost:<port>/swagger`

This provides an interactive interface to test all API endpoints.

## Example Requests

### Create a Book

```bash
POST /api/books
Content-Type: application/json

{
  "title": "1984",
  "isbn": "978-0-452-28423-4",
  "publishedAt": "1949-06-08T00:00:00",
  "authorId": 1,
  "categoryId": 1,
  "isRead": false
}
```

### Get Books by Author

```bash
GET /api/books?authorId=1
```

### Partial Update (PATCH)

```bash
PATCH /api/books/1
Content-Type: application/json

{
  "isRead": true
}
```

## Error Responses

The API returns standard HTTP status codes:

- `200 OK` - Successful request
- `201 Created` - Resource created successfully
- `400 Bad Request` - Validation error or invalid input
- `404 Not Found` - Resource not found
- `409 Conflict` - Resource already exists (e.g., duplicate ISBN)

Error responses follow this format:

```json
{
  "message": "Error description"
}
```

## Development Notes

### Technical Details

- **Database**: SQLite database file (`book-library.db`) is created automatically in the project root
- **Timestamps**: All timestamps use `DateTime.Now` (local time)
- **HTTPS**: The API uses HTTPS redirection in production
- **Logging**: Structured logging using `ILogger<T>` with event IDs defined in `LogEvents`
- **Null Safety**: The project uses nullable reference types (`<Nullable>enable</Nullable>`)
- **Modern C#**: Uses C# 12 features including:
  - Primary constructors
  - Required properties
  - File-scoped namespaces
  - Implicit usings

### Key Design Decisions

1. **Generic Service Pattern**: `DbService<T>` reduces code duplication across all entity types
2. **DTO Hierarchy**: BaseDTO → SetDTO → Entity pattern provides flexibility for different operations
3. **Interface Segregation**: Separate interfaces for each service (`IBookService`, `IAuthorService`, etc.)
4. **Reflection-Based Updates**: The `Update` method uses reflection to copy properties from DTO to entity
5. **Validation**: Manual validation in controllers ensures business rules are enforced

### Known Limitations

- **Partial Updates**: The reflection-based update method cannot set values to `null` (only non-null values are copied)
- **Error Handling**: Basic error handling; global exception middleware could be added
- **Search**: Exact match filtering only; no full-text search or fuzzy matching
- **Pagination**: No pagination support for list endpoints

## Future Enhancements

Potential improvements for this project:

### Error Handling & Validation

- Global exception handling middleware
- Problem Details (RFC 7807) for standardized error responses
- Custom exception types (`NotFoundException`, `ConflictException`, etc.)
- Enhanced model validation with custom validation attributes

### Features

- Authentication and authorization (JWT, OAuth, etc.)
- Pagination for list endpoints
- Full-text search capabilities
- Fuzzy matching for book titles and author names
- Book cover image support
- Reading progress tracking
- Book ratings and reviews
- Export/import functionality (CSV, JSON)

### Technical Improvements

- Unit tests and integration tests
- API versioning
- Caching for frequently accessed data
- Database migrations with EF Core migrations
- Health checks endpoint
- Rate limiting
- Request/response logging middleware

## Learning Outcomes

By studying or working with this project, you'll gain experience with:

1. **RESTful API Design** - Proper HTTP methods, status codes, and resource naming
2. **Entity Framework Core** - ORM usage, DbContext, DbSet, LINQ queries
3. **Dependency Injection** - Service registration, constructor injection, scoped lifetimes
4. **Async Programming** - Async/await patterns, Task-based operations
5. **LINQ** - Query syntax, filtering, projection, deferred execution
6. **DTOs and Validation** - Data transfer objects, model validation, separation of concerns
7. **Generic Programming** - Generic classes, constraints, code reuse
8. **Modern C#** - Latest language features and best practices

## License

This is an example project for educational purposes. Feel free to use it as a learning resource or starting point for your own projects.
