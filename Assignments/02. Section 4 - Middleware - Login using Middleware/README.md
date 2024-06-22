# LoginSolution

This project is a simple middleware-based login solution for an ASP.NET Core application.

## Project Structure

- `Program.cs`: Entry point of the application.
- `Middlewares/ValidateLoginMiddleware.cs`: Middleware to validate login credentials.

## Middleware Functionality

The `ValidateLoginMiddleware` checks for email and password in the request body. If the credentials are valid, it returns a success message; otherwise, it returns an error.

## Usage

1. Clone the repository.
2. Build the solution.
3. Run the application.
4. Send a POST request to the root URL with `email` and `password` in the body to test the login.

## Testing

Tested using Postman:

- URL: `http://localhost:5106/`
- Method: `POST`
- Body: `x-www-form-urlencoded`
  - `email: admin@example.com`
  - `password: admin1234`

## Course Details

- **Course Name**: ASP.NET Core 8 (.NET 8) | True Ultimate Guide
- **Section Name**: Section 3 - HTTP - Login Solution
- **Assignment Name**: LoginSolution