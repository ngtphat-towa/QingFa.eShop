# Qingfa.eShop

Welcome to **Qingfa.eShop**! This is a modern eCommerce platform built with .NET 8 and PostgreSQL, designed to manage online stores efficiently. The application includes features for product management, user authentication, order processing, and secure payment handling through Stripe.

## Project Overview

| **Feature**          | **Description**                                      |
|----------------------|------------------------------------------------------|
| **Product Management** | Manage product listings, categories, and details.  |
| **User Authentication** | User registration, login, and account management.  |
| **Order Processing**  | Track and manage customer orders and order status. |
| **Payment Integration** | Process payments securely with Stripe.             |

## Prerequisites

Ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Visual Studio Code](https://code.visualstudio.com/)

## Setup and Installation

1. **Clone the Repository**

    ```bash
    git clone https://github.com/ngtphat-towa/Qingfa.eShop.git
    cd Qingfa.eShop
    ```

2. **Docker Setup**

    1. **Ensure Docker is Running**

        Check if Docker is running:

        ```bash
        docker --version
        ```

    2. **Run PostgreSQL Using Docker**

        Create a `docker-compose.yml` file:

        ```yaml
        version: '3.8'

        services:
          postgres:
            image: postgres:latest
            container_name: qingfa-postgres
            environment:
              POSTGRES_USER: qingfa_user
              POSTGRES_PASSWORD: qingfa_password
              POSTGRES_DB: qingfa_db
            ports:
              - "5432:5432"
            volumes:
              - postgres_data:/var/lib/postgresql/data

          # Uncomment for Stripe CLI
          # stripe:
          #   image: stripe/stripe-cli:latest
          #   container_name: stripe
          #   environment:
          #     STRIPE_SECRET_KEY: your_stripe_secret_key
          #   ports:
          #     - "8000:8000"

        volumes:
          postgres_data:
        ```

        Start Docker containers:

        ```bash
        docker-compose up -d
        ```

    3. **Verify PostgreSQL Container**

        Check if PostgreSQL container is running:

        ```bash
        docker ps
        ```

3. **Configure the Application**

    1. **Update Connection String**

        Modify `appsettings.json` with your PostgreSQL details:

        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Host=localhost;Port=5432;Database=qingfa_db;Username=qingfa_user;Password=qingfa_password;"
        }
        ```

    2. **Configure Stripe**

        Add your Stripe secret key in a `.env` file or as an environment variable:

        ```env
        STRIPE_SECRET_KEY=your_stripe_secret_key
        ```

    3. **Run Migrations**

        Apply database migrations:

        ```bash
        dotnet ef database update
        ```

4. **Run the Application**

    Build and run the application:

    ```bash
    dotnet run
    ```

    Access the application at `http://localhost:5000`.

## Usage

- **Admin Dashboard:** Manage products, users, and orders at `/admin`.
- **Customer Interface:** Browse and shop at `/shop`.
- **Payment Processing:** Handled securely through Stripe.
- **API Documentation:** Available at `https://localhost:5001/api/docs`.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact and Support

For support or questions:

- **Email:** ruanqingfa@gmail.com
- **Issue Tracker:** [GitHub Issues](https://github.com/ngtphat-towa/Qingfa.eShop/issues)
