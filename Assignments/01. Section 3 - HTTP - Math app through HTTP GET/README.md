# CalculatorSolution

This is a simple ASP.NET Core application that performs basic arithmetic operations using HTTP GET requests. The application takes query parameters for two numbers and an operation, and returns the result of the operation.

## Course Information

- **Course Name**: Asp.Net Core 8 (.NET 8) | True Ultimate Guide
- **Section Name**: 01. Section 3 - HTTP - Math app through HTTP GET
- **Assignment Name**: CalculatorSolution

## How to Use

### Running the Application

1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Run the application using the following command:

    ```bash
    dotnet run
    ```

4. The application will start and listen for HTTP GET requests.

### Testing with Postman

This application is tested using Postman. You can use Postman to send GET requests with the required query parameters to test the various operations.

### Query Parameters

- `firstNumber`: The first number for the operation (required).
- `secondNumber`: The second number for the operation (required).
- `operation`: The arithmetic operation to perform (required). Supported operations are `add`, `subtract`, `multiply`, `divide`, and `modulus`.

### Example Requests

- **Addition**

    ```
    GET http://localhost:5000?firstNumber=10&secondNumber=5&operation=add
    ```

- **Subtraction**

    ```
    GET http://localhost:5000?firstNumber=10&secondNumber=5&operation=subtract
    ```

- **Multiplication**

    ```
    GET http://localhost:5000?firstNumber=10&secondNumber=5&operation=multiply
    ```

- **Division**

    ```
    GET http://localhost:5000?firstNumber=10&secondNumber=5&operation=divide
    ```

- **Modulus**

    ```
    GET http://localhost:5000?firstNumber=10&secondNumber=5&operation=modulus
    ```

# License
This project is licensed under the MIT License.