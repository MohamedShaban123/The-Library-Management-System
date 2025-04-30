Library Management System

The **Library Management System** is a .NET 8-based application designed to streamline library operations. It provides a robust API for managing books, users, and borrowing records, ensuring efficient and secure library management.

## Features

- **Book Management**: Add, update, delete, and retrieve book records.
- **User Management**: Handle user registration, authentication, and role-based access control using ASP.NET Identity.
- **Borrowing System**: Allow users to borrow books, track due dates, and manage borrowing history.
- **Data Seeding**: Preload the database with initial data for books and borrowed records.
- **Secure Authentication**: Implements JWT-based authentication and role-based authorization.
- **Swagger Integration**: API documentation and testing using Swagger UI.

## Design Patterns Used

1. **Repository Pattern**: Abstracts database operations for better maintainability.
2. **Unit of Work Pattern**: Ensures transactional consistency across multiple operations.
3. **Dependency Injection**: Promotes loose coupling and testability.
4. **Builder Pattern**: Configures and builds the application pipeline.
5. **Mapper Pattern**: Simplifies object mapping using AutoMapper.
6. **Strategy Pattern**: Configures authentication strategies (e.g., JWT).

## Technologies Used

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **SQL Server** (or any configured database)
- **AutoMapper** for object mapping
- **Swagger** for API documentation

## Prerequisites

- .NET 8 SDK installed
- SQL Server or any compatible database
- Visual Studio 2022 or any compatible IDE

## Installation

1. Clone the repository:
   git clone https://github.com/MohamedShaban123/The-Library-Management-System
2. Navigate to the project directory:
cd library-management-system
3. Restore dependencies:
dotnet restore
4. Update the database connection string in `appsettings.json`:
"ConnectionStrings": "LibraryConnectionToDatabase": "Server=DESKTOP-D8L1JHB\\MSSQLSERVER1; Database= Library Management System; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True; "
5. Apply database migrations:
dotnet ef database 


## Usage

1. Run the application:
dotnet run

2. Access the API at `https://localhost:5001` (or the configured URL).
3. Use tools like Postman or Swagger to interact with the API.

## API Endpoints

### Books
- `GET /api/books` - Retrieve all books
- `POST /api/books` - Add a new book
- `PUT /api/books/{id}` - Update a book
- `DELETE /api/books/{id}` - Delete a book

### Borrowing
- `POST /api/borrow/{bookId}` - Borrow a book
- `GET /api/borrow` - Retrieve borrowed books

### Users
- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login

## Project Structure

- **Core**: Contains entities and shared logic.
- **Repository**: Implements the Unit of Work and repository patterns.
- **APIs**: Contains controllers and API logic.
- **Service**: Business logic and service layer.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.

## Contact

For questions or support, please contact [mohammedshaban1458@gmail.com].
