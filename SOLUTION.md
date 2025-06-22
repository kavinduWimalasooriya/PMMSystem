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

## Role-Based Access Control and History Management ##

To ensure that only authorized users can update the Status field of a maintenance card, I've implemented a two-tiered security mechanism, involving both the front-end (Angular) and back-end (API).

Front-End Implementation (Angular):

- Upon user login, the current user's role is securely stored in the browser's local storage.
- This stored role is then exposed as an Angular Signal within the application.
- In the maintenance-edit component, I subscribe to this signal to retrieve the current user's role.
- Based on the retrieved role, the Status update field is conditionally disabled if the user does not possess "Admin" privileges, preventing unauthorized modifications at the UI level.
  
Back-End Implementation (API):

- When an update request for a maintenance card is received, the user's role is extracted from the incoming form data.
- A server-side validation check is performed: if a PropertyManager attempts to alter the Status field, a custom <code>MockAuthException()</code> is deliberately thrown.
- This exception is then gracefully caught by the global exception handler, which translates it into an appropriate HTTP status code (e.g., 403 Forbidden) and sends it back to the front-end, indicating an unauthorized operation.

This layered approach provides robust security, preventing unauthorized status changes both visually on the front end and definitively at the API level.

## Endpoint Testing Improvements ##
When testing the updated endpoint on Scalar, please ensure the status and role fields are populated with the correct values.

Current Valid Values:

- status:  "New", "Accepted", "Rejected",  or their corresponding integer representations (0, 1, 2).
- role:  "Admin" or "PropertyManager",  or their corresponding integer representations (0, 1).
  
## Proposed System Enhancements ##
We can make two significant improvements to our system:

1. Token-Based Authentication System
Our current system utilizes token-based authentication, and it's essential to ensure its continued proper function and security.

2. Migrate Database from SQLite to MS SQL Server
We need to migrate our database from SQLite to a more robust solution like MS SQL Server. This will provide better scalability, performance, and features for our application.
