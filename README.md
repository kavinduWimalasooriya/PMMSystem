# Property Maintenance Management System Setup Guide #
This guide provides a concise setup for your .NET 9 Web API backend and Angular 19 frontend.

## Getting Starte : Prerequisites
Ensure you have these installed:

Git: Download Git

.NET 9 SDK: Download .NET 9 SDK

Node.js (LTS) & npm: Download Node.js

Angular CLI

### Step 1: Clone the Repository
Open your terminal.

Navigate to your desired project directory.

Clone the repository:

git clone https://github.com/kavinduWimalasooriya/PMMSystem.git

<code>cd PMMSystem</code>

### Step 2: Backend Setup (.NET 9 Web API)
Navigate:
<code> cd PMMSystem.API </code>

Install & Build:
<code>dotnet restore
dotnet build</code>

Run:
<code>dotnet run</code>

(API typically runs on https://localhost:5001). Access Scaler at /(https://localhost:5001/scalar/v1).

### Step 3: Frontend Setup (Angular 19)
Navigate:
<code>cd ..
cd client</code>

Install & Build:
<code>npm install # or npm i
ng build</code>

Run:
<code>ng serve</code>

(Frontend typically runs on http://localhost:4200).

## Architecture And Project Structure ##

My project is built upon the Clean Architecture principles, promoting a robust and maintainable codebase. The entire system is logically divided into four distinct layers:

- Domain Layer: This layer embodies the core business logic and entities. Crucially, it is independent of all other layers, ensuring that changes in external concerns (like databases or UI) do not impact the fundamental business rules.

- Application Layer: This layer orchestrates the use cases and application-specific business rules. It defines what the application does. It depends solely on the Domain Layer, utilizing its entities and business logic to implement application functionalities.

- Infrastructure Layer: This layer provides the technical implementations for interfaces defined in the Application and Domain Layers. It handles concerns like data persistence, external service integrations, and cross-cutting concerns. The Infrastructure Layer depends on both the Application and Domain Layers to fulfill its responsibilities.

- Presentation Layer: This layer is responsible for presenting information to the user and handling user interactions. It acts as the entry point for user requests. The Presentation Layer has knowledge of the Application, Domain, and Infrastructure Layers to coordinate the flow of data and application logic for user-facing features.

By strictly adhering to Clean Architecture, I've achieved a high degree of separation of concerns, significantly enhancing the project's maintainability, testability, and flexibility.

To further bolster these architectural advantages, I've implemented:

- Dependency Injection (DI): This design pattern is used extensively to achieve Inversion of Control (IoC). DI promotes loose coupling between components, making the system more modular and easier to test.
- Repository Pattern: This pattern abstracts the data access logic, providing a clean API for the Application Layer to interact with data sources without knowing the underlying implementation details.

For data access and storage, I've opted for a straightforward setup using Entity Framework Core with SQLite. This combination offers a lightweight yet powerful solution for managing relational data.

Project documentation is provided through OpenAPI with Scalar, ensuring comprehensive and interactive API specifications.

Key additional packages utilized include:

- Entity Framework Core: For object-relational mapping (ORM) and data persistence.
- AutoMapper: To simplify object-to-object mapping, reducing boilerplate code when transferring data between layers (e.g., DTOs to Domain Entities).
- Scalar: For enhanced OpenAPI documentation rendering.

To ensure a resilient and user-friendly application, the project incorporates robust global error handling for both front-end (if applicable, or implies a unified backend error response for client consumption) and back-end operations. Furthermore, a comprehensive logging system powered by NLog is integrated to facilitate monitoring, debugging, and operational insights.
