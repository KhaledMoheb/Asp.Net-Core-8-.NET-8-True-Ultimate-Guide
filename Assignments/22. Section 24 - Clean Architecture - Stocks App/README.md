# StockMarketSolution

This ASP.NET Core Web Application displays live stock prices with updates from [finnhub.io](https://finnhub.io/).

## Overview

This project extends the assignment from **21. Section 23 - SOLID Principles - Stocks App**

## Changes from previous assignment:

- Use clean architecture.

## Instructions Followed throughout the assignments regarding Stocks App

1. Create controller(s) with attribute routing.

2. Provide the configuration as service, using Options pattern.

3. Inject the IOptions in the controller.

4. Use CSS styles, layout views, _ViewImports, _ViewStart as per the necessity.

5. The CSS file is provided as downlodable resource for applying essential styles. You can download and use it.

6. Inject essential services such as IFinnhubCompanyProfileService and other services in Controller. Invoke IStocksRepository and IFinnhubRepository in respective service classes.

7. Invoke essential service methods in controller.

8. The Entity model class (BuyOrder and SellOrder) should not be accessed in the controller. They must be used only in the service classes.

9. The DTO model classes (BuyOrderRequest, BuyOrderResponse, SellOrderRequest, SellOrderResponse) should be used as parameter type or return type in the service methods; and can be used in both services and controller.

10. Use appropriate tag helpers such as "asp-controller", "asp-action", "asp-for" etc., in all views wherever necessary.

11. Write appropriate logs using ILogger, in controllers and services.

12. In case of non-Development environment, apply both custom exception handling middleware (i.e. ExceptionHandlingMiddleware) and also built-in error handling middleware called "UseExceptionHandler".

13. Apply SOLID principles such as 'Interface Segregation Principle', 'Single Responsibility Principle', 'Dependency Inversion Principle' and other principles while creating services and other classes.

14. Use clean architecture to create layers of this application.

## Features

- Real-time stock price updates from [finnhub.io](https://finnhub.io).
- Buy and sell order management with business rule validation.
- xUnit tests for validating service layer logic.
- xUnit tests for validating controllers.
- Action Filters
- Exception Filters
- SOLID Principles

## Setup

1. **Clone the repository**

2. **Open using Visual Studio Community**

2. **Configure user-secrets** with your Finnhub token:
    ```sh
    dotnet user-secrets init
    dotnet user-secrets set "FinnhubToken" "your-api-token"
    ```

## Running the Tests

The project includes xUnit tests for validating the functionality of the `StocksService` class. To run the tests, follow these steps:

1. Open the solution in Visual Studio.
2. Open Test Explorer from the View menu.
3. Select "Run All Tests In View" to execute the tests.

## Example Tests

### Buy Order Tests

- Validate null requests.
- Validate quantity and price constraints.
- Validate stock symbol and date constraints.
- Ensure proper response for valid requests.

### Sell Order Tests

- Similar validations as buy orders.

## Assignment Details

- **Course Name**: Asp.Net Core 8 (.NET 8) True Ultimate Guide
- **Section Name**: 22. Section 24 - Clean Architecture - Stocks App
- **Assignment Name**: StockMarketSolution
