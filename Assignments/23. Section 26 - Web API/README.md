# WebAPI Project

This project is part of Asp.Net Core 8 (.NET 8) True Ultimate Guide course. It demonstrates how to create a Web API using ASP.NET Core, implementing CRUD operations for Orders and OrderItems, and includes unit tests using AutoFixture, Moq, and FluentAssertions.

## Table of Contents

- [Assignment Details](#assignment-details)
- [Project Structure](#project-structure)
- [Setup Instructions](#setup-instructions)
- [Endpoints](#endpoints)
- [Swagger Documentation](#swagger-documentation)
- [Unit Tests](#unit-tests)
- [License](#license)

## Assignment Details

- **Course Name**: Asp.Net Core 8 (.NET 8) True Ultimate Guide
- **Section Name**: 23. Section 26 - Web API
- **Assignment Name**: Web API

## Project Structure

The project is organized into the following main directories:

- `WebAPI.Core`: Contains core business logic, DTOs, and service contracts.
- `WebAPI.Infrastructure`: Contains the database context and repository implementations.
- `WebAPI.Web`: Contains the Web API controllers and startup configuration.
- `WebAPI.Controllers.Tests`: Contains the unit tests for the controllers.

## Setup Instructions

Follow these steps to set up and run the project:

1. **Clone the Repository:**

2. **Configure the Database:**

    Update the connection string in `appsettings.json` in the `WebAPI.Web` project to point to your SQL Server instance.

    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
    }
    ```

3. **Run Migrations:**

    Apply the Entity Framework Core migrations to create the database schema.

    ```bash
    dotnet ef database update --project WebAPI.Infrastructure --startup-project WebAPI.Web
    ```

4. **Run the Project:**

    Start the Web API project.

    ```bash
    dotnet run --project WebAPI.Web
    ```

5. **Access the API:**

    The API will be available at `https://localhost:5242`.

## Endpoints

### OrdersController

- `GET /api/orders`: Retrieves all orders.
- `GET /api/orders/{id}`: Retrieves an order by its ID.
- `POST /api/orders`: Creates a new order.
- `PUT /api/orders/{id}`: Updates an existing order.
- `DELETE /api/orders/{id}`: Deletes an order by its ID.

### OrderItemsController

- `GET /api/orderitems`: Retrieves all order items.
- `GET /api/orderitems/{id}`: Retrieves an order item by its ID.
- `GET /api/orderitems/order/{orderId}`: Retrieves all order items of a specific order ID.
- `POST /api/orderitems`: Creates a new order item.
- `PUT /api/orderitems/{id}`: Updates an existing order item.
- `DELETE /api/orderitems/{id}`: Deletes an order item by its ID.

## Swagger Documentation

Swagger is enabled to provide interactive API documentation. Access it at:

## Unit Tests

Unit tests are implemented using AutoFixture, Moq, and FluentAssertions. They cover various scenarios for the `OrdersController` and `OrderItemsController`.

To run the tests, use the following command:

```bash
dotnet test
```

## License
This project is licensed under the MIT License.