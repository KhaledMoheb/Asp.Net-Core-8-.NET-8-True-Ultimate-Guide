# Bank Solution

This project demonstrates basic functionality of a banking application using ASP.NET Core MVC controllers.

## Course Information

- **Course Name:** Asp.Net Core 8 (.NET 8) | True Ultimate Guide
- **Section Name:** 04. Section 6 - Controllers - Bank app using Controllers
- **Assignment Name:** BankSolution

## Application Overview

The BankSolution project showcases a simple banking application with the following features:

- **Index Route:** Displays a welcome message.
- **AccountDetails Route:** Returns account details as JSON.
- **AccountStatement Route:** Provides a sample PDF file for download.
- **GetCurrentBalanceOfAccountNumer Route:** Retrieves the current balance based on the provided account number.
- **GetCurrentBalance Route:** Handles requests for retrieving current balance without specifying an account number.

## Usage

### Prerequisites

- .NET 8 SDK or later
- Visual Studio 2019 or Visual Studio Code

### Running the Application

1. Clone this repository.

   ```bash
   git clone https://github.com/KhaledMoheb/Asp.Net-Core-8-.NET-8-True-Ultimate-Guide.git

2. Navigate to the project directory.

   ```bash
    cd Asp.Net-Core-8-.NET-8-True-Ultimate-Guide/Assignments/04. Section 6 - Controllers - Bank app using Controllers/BankSolution

3. Restore dependencies and run the application.

    dotnet restore
    dotnet run

4. Open a web browser and navigate to http://localhost:5216 to interact with the application.

### Testing

The application functionalities can be tested using tools like Postman or any HTTP client to send requests to the specified routes.

### Additional Notes

Ensure that the docs.pdf file exists in the project root or update the File method call in AccountStatement action to point to the correct file path.

### Notes:

- Replace `docs.pdf` with the actual path to the PDF file if it exists in another location within your project structure.
- Adjust the port number (`5216`) in the URLs if your application runs on a different port.