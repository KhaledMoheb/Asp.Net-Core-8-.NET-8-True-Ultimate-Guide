# StockMarketSolution

This ASP.NET Core Web Application displays live stock prices with updates from [finnhub.io](https://finnhub.io/).

## UI Design

The main view is `Trade/Index`, where live stock prices are displayed and updated in real-time.

## Architecture

The project uses an n-layer (n-tier) architecture, which separates concerns into distinct layers for better maintainability and scalability.

## Finnhub.io

[finnhub.io](https://finnhub.io) is a service provider that offers live stock price information online.

## User-Secrets

To use the Finnhub API, register at [finnhub.io/login](https://finnhub.io/login) to generate your own token.

After registration, you can find your API Key (token) at [finnhub.io/dashboard](https://finnhub.io/dashboard).

Store this token in user-secrets on your machine. This token is attached to the request URL when making requests to Finnhub.

Store the secret "FinnhubToken" using:

```sh
dotnet user-secrets init

dotnet user-secrets set "FinnhubToken" "api token string"
``` 

## Assignment Details

Course Name: Asp.Net Core 8 (.NET 8) True Ultimate Guide
Section Name: 12. Section 14 - Configuration - Stocks App
Assignment Name: StockMarketSolution