# Match Management Web API

## Overview
This project is a Match Management Web API built using .NET 8.0 with SQL Server and Entity Framework Core. It provides CRUD operations for managing matches and their associated odds. The API includes endpoints for creating, reading, updating, and deleting matches, as well as managing match odds. Documentation for the API is provided using Swagger.

### New Additions
In addition to the existing technologies, the project now includes integrations with Elasticsearch, Kibana, Redis, Serilog, and Docker Compose profiles for flexible deployment options.

## Features
- CRUD API for Match entity
- CRUD API for MatchOdds entity
- Support for multiple sports (Football, Basketball) using enums
- Integration with AutoMapper for mapping between DTOs and entities
- Docker support for running the API and associated services in containers
- Swagger integration for API documentation and interaction
- **New**: Integration with Elasticsearch and Kibana for search and visualization
- **New**: Utilization of Redis for caching to optimize data retrieval and application performance
- **New**: Logging implemented with Serilog for structured logging
- **New**: Docker Compose profiles for flexible deployment configurations

## Project Structure
The solution is structured into the following projects:

1. **API**: Web API project responsible for handling HTTP requests and responses.
2. **Services**: Class Library project containing business logic and services for managing matches and match odds.
3. **Infrastructure**: Class Library project providing data access and database interactions using Entity Framework Core.
4. **Core**: Class Library project containing shared models, enums, and other common functionalities.

### New Components
- **Elasticsearch**: Integrated for scalable and real-time search capabilities.
- **Kibana**: Utilized for visualizing Elasticsearch data and creating dashboards.
- **Redis**: Employed as a caching layer to optimize data retrieval and application performance.
- **Serilog**: Used for structured logging to capture detailed log events.
- **Docker Compose Profiles**: Configurations to control which services are started together for different deployment scenarios.

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
The API and associated services can be run in Docker containers for easy deployment and scalability. Dockerfile and docker-compose configurations are provided for building and running the containerized application.

### Docker Compose Profiles
This project uses Docker Compose profiles to control deployment configurations:

- **docker-api**: Starts all services including the API, database (SQL Server), Elasticsearch, Kibana, Redis, and other necessary services.
- **iisexplorer-api**: Starts only essential services excluding IIS Express for the API, useful for running Elasticsearch, Kibana, Redis, and other services independently for development or testing.

## Swagger Documentation
API documentation and interaction are facilitated through Swagger. After running the API, you can access the Swagger UI to explore and interact with the available endpoints. The Swagger UI provides a user-friendly interface for testing API operations and understanding their functionality.

## Setup and Usage
1. Clone the repository to your local machine.
2. Navigate to the API project directory.
3. Build and run the project using `dotnet run`.
4. Access the Swagger UI by navigating to `/swagger` in your web browser.

### Running with Docker Compose
To run the project using Docker Compose and specific profiles, use the following commands:

- **Start all services (docker-api profile)**:
  ```bash
  docker-compose --profile docker-api up
  
- **Start essential services only(iisexplorer-api profile)**:
  ```bash
  docker-compose --profile docker-api up
  
## Dependencies
- .NET 8.0
- Entity Framework Core
- AutoMapper
- Docker (for containerization)
- SQL Server (for database)
- New: Elasticsearch and Kibana (for search and visualization)
- New: Redis (for caching)
- New: Serilog (for structured logging)

## Future Work
- **Add Test Project**: Develop a test project to cover unit tests, integration tests, and end-to-end tests for ensuring the reliability and correctness of the API functionality. Implementing test automation will contribute to maintaining code quality and facilitating future development and enhancements.
