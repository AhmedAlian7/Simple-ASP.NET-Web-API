# Product Management API

A RESTful API built with C# (.NET 9) for managing products, users, and orders. This project uses Entity Framework Core for data access and follows a clean architecture with service and controller layers.

## Features

- CRUD operations for Products
- User management and permissions
- Order management
- DTO-based data transfer
- Asynchronous database operations

## Technologies

- C# 13.0
- .NET 9
- Entity Framework Core
- ASP.NET Core Web API


### API Endpoints

#### Products

- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Add a new product
- `PUT /api/products/{id}` - Update a product
- `DELETE /api/products/{id}` - Delete a product

#### Users & Orders

- Similar endpoints exist for users and orders (see respective controllers).


## Project Structure

- `Controllers/` - API controllers
- `Services/` - Business logic and data access
- `Models/` - Entity and DTO classes
- `Data/` - Database context and configurations
