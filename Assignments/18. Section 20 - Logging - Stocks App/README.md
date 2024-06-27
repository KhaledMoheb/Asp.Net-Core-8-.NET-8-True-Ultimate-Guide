# StockMarketSolution

This ASP.NET Core Web Application displays live stock prices with updates from [finnhub.io](https://finnhub.io/).

## Overview

This project extends the assignment from **17. Section 19 - xUnit Advanced - Stocks App**

## Changes from previous assignment:

- Adding logs using Serilog.
- Added TradeControllerIntegrationTest
- Added StocksControllerTest

## Features

- Real-time stock price updates from [finnhub.io](https://finnhub.io).
- Buy and sell order management with business rule validation.
- xUnit tests for validating service layer logic.

## Setup

1. **Clone the repository**:
    ```sh
    git clone https://github.com/your-repo/StockMarketSolution.git
    cd StockMarketSolution
    ```

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
- **Section Name**: 18. Section 20 - Logging - Stocks App
- **Assignment Name**: StockMarketSolution
