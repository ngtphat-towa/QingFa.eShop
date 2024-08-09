# Qingfa.eShop

**Qingfa.eShop** is a .NET 8-based eCommerce platform using Clean Architecture principles. This application allows for robust and scalable design patterns for online shopping platforms.

## Table of Contents

- [Project Overview](#project-overview)
- [Prerequisites](#prerequisites)
- [Setup and Installation](#setup-and-installation)
- [Project Structure](#project-structure)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## Project Overview

The application follows Clean Architecture principles, dividing the solution into several layers:

- **`Qingfa.eShop.Api`**: The API layer that handles HTTP requests and responses.
- **`Qingfa.eShop.Application`**: The application layer containing business logic and use cases.
- **`Qingfa.eShop.Domain`**: The domain layer containing core business rules and entities.
- **`Qingfa.eShop.Infrastructure`**: The infrastructure layer handling data access and external services.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Visual Studio Code](https://code.visualstudio.com/)

## Setup and Installation

1. **Clone the Repository**

    ```bash
    git clone https://github.com/ngtphat-towa/Qingfa.eShop.git
    cd Qingfa.eShop
    ```

2. **Run Docker Containers**

    ```bash
    docker-compose up -d
    ```

3. **Restore and Build the Solution**

    ```bash
    dotnet restore
    dotnet build
    ```

4. **Run the Application**

    ```bash
    dotnet run --project Qingfa.eShop.Api
    ```

## Project Structure

- **`Qingfa.eShop.Api`**: Handles HTTP requests and API endpoints.
- **`Qingfa.eShop.Application`**: Contains business logic and application services.
- **`Qingfa.eShop.Domain`**: Defines core business entities and domain services.
- **`Qingfa.eShop.Infrastructure`**: Manages data access and external integrations.

## Testing

1. **Unit Tests**

    Unit tests are located in the `Qingfa.eShop.Tests` project. To run the tests:

    ```bash
    dotnet test Qingfa.eShop.Tests
    ```

2. **Integration Tests**

    Integration tests can be run similarly to unit tests but may require additional setup for external services and databases.

## Contributing

To contribute to this project, please fork the repository, create a new branch, and submit a pull request. Ensure your changes adhere to the project's coding standards and pass all tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For any questions or support, please reach out to [support@qingfa.com](mailto:ruanqingfa@gmail.com).
