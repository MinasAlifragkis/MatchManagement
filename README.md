# Match Management Web API

## Overview
This project is a Match Management Web API built using ASP.NET Core 3.1 with SQL Server and Entity Framework Core. It provides CRUD operations for managing matches and their associated odds. The API includes endpoints for creating, reading, updating, and deleting matches, as well as managing match odds. Documentation for the API is provided using Swagger.

## Features
- CRUD API for Match entity
- CRUD API for MatchOdds entity
- Support for multiple sports (Football, Basketball) using enums
- Integration with AutoMapper for mapping between DTOs and entities
- Docker support for running the API in a Linux container
- Swagger integration for API documentation and interaction

## Project Structure
The solution is structured into the following projects:

1. **API**: Web API project responsible for handling HTTP requests and responses.
2. **Services**: Class Library project containing business logic and services for managing matches and match odds.
3. **Infrastructure**: Class Library project providing data access and database interactions using Entity Framework Core.
4. **Core**: Class Library project containing shared models, enums, and other common functionalities.

## Entities
### Match
- **Id**: Unique identifier for the match.
- **Description**: Description of the match.
- **MatchDate**: Date of the match.
- **MatchTime**: Time of the match.
- **TeamA**: Name or identifier of Team A.
- **TeamB**: Name or identifier of Team B.
- **Sport**: Enum representing the type of sport (Football = 1, Basketball = 2).

### MatchOdds
- **Id**: Unique identifier for the match odds.
- **MatchId**: Identifier of the associated match.
- **Specifier**: Description or specifier for the odds.
- **Odd**: Value of the odds.

## Docker
The API can be run in a Linux Docker container for easy deployment and scaling. Dockerfile and docker-compose configurations are provided for building and running the containerized application.

## Swagger Documentation
API documentation and interaction are facilitated through Swagger. After running the API, you can access the Swagger UI to explore and interact with the available endpoints. The Swagger UI provides a user-friendly interface for testing API operations and understanding their functionality.

## Setup and Usage
1. Clone the repository to your local machine.
2. Navigate to the API project directory.
3. Build and run the project using `dotnet run`.
4. Access the Swagger UI by navigating to `/swagger` in your web browser.

## Dependencies
- ASP.NET Core 3.1
- Entity Framework Core
- AutoMapper
- Docker (for containerization)
- SQL Server (for database)
