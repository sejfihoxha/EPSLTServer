
---

## Features

- Generate unique discount codes of configurable length (7-8 characters).
- Save discount codes to a database with uniqueness constraint.
- Use/consume discount codes through TCP.
- Clean separation of concerns following **Clean Architecture**.
- Entity Framework Core for data persistence.
- Supports batch generation with duplicate detection.

---


## Setup Instructions

### 1. Prerequisites

- [.NET 8 SDK]
- SQL Server (e.g., `localhost\SQLEXPRESS`)

### 2. Configure Connection String

Edit the `AddDiscountCodeDb` method in `Program.cs` and replace the connection string with your own:

### 3. Migrate DB

Inside the `ESPSLTServer.Infrastructure` solution, execute the following commands:

```bash
add-migration "InitialDb"
update-database


This will set up the database.

After that, you can run the server.

Warning: The server must be running in order to send requests from the client.
