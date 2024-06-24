# ConfigurationDemoSolution

This ASP.NET Core application demonstrates the use of configuration settings to manage displayed social media links.

## Routes

- `/`: Displays social media links configured in the appsettings.json file.

## Usage

To run the application locally:
1. Ensure you have .NET 6 SDK installed.
2. Clone the repository.
3. Navigate to the project directory.
4. Run the application using `dotnet run`.

## Running the Application

### Run as Production

To run the application with the Production environment:

```sh
dotnet run --launch-profile "Production"
```

### Run as Development

To run the application with the Development environment:

```sh
dotnet run --launch-profile "Development"
```

### Configuration

The application reads social media links from the configuration files:

appsettings.json
appsettings.Development.json

### Course Information

- Course Name: Asp.Net Core 8 (.NET 8) True Ultimate Guide
- Section Name: 11. Section 14 - Configuration - Social Media Links
- Assignment Name: ConfigurationDemoSolution
