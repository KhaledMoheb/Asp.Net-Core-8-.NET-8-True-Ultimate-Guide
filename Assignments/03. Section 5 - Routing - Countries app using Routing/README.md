# Countries Solution

This ASP.NET Core application provides endpoints to retrieve country names based on their IDs.

## Endpoints

### Get Country by ID

- **Endpoint**: `GET /country/{id}`
  
  Retrieves the name of a country based on the provided ID.

  - **Parameters**:
    - `id`: Integer representing the country ID.

  - **Responses**:
    - **200 OK**: Returns the country name for valid IDs (1-5).
    - **404 Not Found**: Returns "[No Country]" for IDs between 6 and 100.
    - **400 Bad Request**: Returns "The CountryID should be between 1 and 100" for IDs greater than 100.

## Usage

1. Start the application using Visual Studio or by running `dotnet run`.
2. Access the endpoints using the base URL `http://localhost:5197`.