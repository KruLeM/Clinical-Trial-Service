# Clinical Trial Service

## Overview

The Clinical Trial Service is a web API built using ASP.NET Core that enables users to manage clinical trials. The service provides endpoints to add new clinical trials to the database, update existing ones, and retrieve details of previously created trials.

## Features

- Add a new clinical trial
- Update an existing clinical trial
- Retrieve a list of saved clinical trials with optional pagination parameters
- Retrieve details of a specific clinical trial by `trialId`
- Retrieve a list of clinical trials filtered by `status`, with optional pagination parameters

## Technologies Used

- ASP.NET Core
- MS SQL database
- MediatR
- FluentValidation
- xUnit for unit testing
- Docker for containerization

---

## Getting Started with Docker

To run the project with a Docker container:

1. Clone the GitHub repository.
2. Set up an MS SQL container in the Docker Desktop app.
3. Set up the database service.
4. Configure the startup project.
5. Run the service.

### 2. Set Up MS SQL Container in Docker Desktop

- If you don't have Docker Desktop installed, you can download it from [Docker's official website](https://www.docker.com/products/docker-desktop/).
- Open the command prompt or PowerShell and run this command to get the MS SQL image:

```powershell
docker pull mcr.microsoft.com/mssql/server
```

- After downloading the MS SQL image, run this command to create an SQL container:

```powershell
docker run --name mssql --env ACCEPT_EULA=Y --env MSSQL_SA_PASSWORD=s4C0mplic@t3D.?1C! -p 1400:1433 -d mcr.microsoft.com/mssql/server:latest
```

- After successfully creating the container, check Docker and ensure that the container is running.

> **Note:** If you change any container creation details, update the `DBConnectionDocker` string accordingly.

### 3. Set Up Database Service

To set up the service:

- Open the `API` project.
- Locate the `Program.cs` file.
- Find the line starting with `builder.Services.AddDbContext` and set the method **GetConnectionString** with `"DBConnectionDocker"` as the input parameter.

> **Note:** If you haven't changed anything, your `DBConnectionDocker` string should look like this:
>
> ```json
> "DBConnectionDocker": "Server=host.docker.internal,1400;Database=ClinicalTrialDB;User Id=sa;Password=s4C0mplic@t3D.?1C!;TrustServerCertificate=True;"
> ```

### 4. Configure the Startup Project

To configure the project to run in this mode:

- In the toolbar, set the startup project as `API` and the debug target as `Container (Dockerfile)`.
- If this option is not available:
  - Right-click on the solution **Clinical Trial Service**.
  - Click on `Configure Startup Projects...`.
  - Under `Common Properties`, select `Multiple startup projects`.
  - For the `API` project: **Action:** `Start`, **Debug Target:** `Container (Dockerfile)`. For all other projects, set the action to **None**.

### 5. Run the Service

---

## Getting Started with MS SQL Server Standalone Instance

To run the project locally:

1. Clone the GitHub repository.
2. Set up an MS SQL Server instance.
3. Update the connection string and set up the database service.
4. Configure the startup project.
5. Run the service.

### 2. Set Up an MS SQL Server Instance

- If you don't have MS SQL Server installed, you can download **SQL Server Express** from [Microsoft's official website](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
- Create a database named `ClinicalTrialDB`.
- Create an SQL user `clinical_user` with the password `SecurePassword123` and grant `sysadmin` privileges.

### 3. Update the Connection String and Set Up Database Service

To update the connection string:

- Open the `API` project.
- Locate the `appsettings.json` file.
- Find the `"DBConnection"` property and update its value according to your database configuration and SQL user.

To set up the service:

- Open the `API` project.
- Locate the `Program.cs` file.
- Find the line starting with `builder.Services.AddDbContext` and set the method **GetConnectionString** with `"DBConnection"` as the input parameter.

> **Note:** Your `DBConnection` string should look like this:
>
> ```json
> "DBConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=SecurePassword123;TrustServerCertificate=True;"
> ```

### 4. Configure the Startup Project

To configure the project to run in this mode:

- In the toolbar, set the startup project as `API` and the debug target as `https`.
- Alternatively:
  - Right-click on the solution **Clinical Trial Service**.
  - Click on `Configure Startup Projects...`.
  - Under `Common Properties`, select `Multiple startup projects`.
  - For the `API` project: **Action:** `Start`, **Debug Target:** `https`. For all other projects, set the action to **None**.

### 5. Run the Service

---

## Upload File Content

This service uses `.json` files to add or update a clinical trial.

### Example of a Valid JSON File

```json
{
   "trialId": "123e",
   "title": "Test 1",
   "startDate": "2025-03-07",
   "participants": 15,
   "status": "Not Started"
}
```

### JSON Schema

```json
{
   "$schema": "http://json-schema.org/draft-07/schema#",
   "title": "ClinicalTrialMetadata",
   "type": "object",
   "properties": {
      "trialId": { "type": "string" },
      "title": { "type": "string" },
      "startDate": { "type": "string", "format": "date" },
      "endDate": { "type": "string", "format": "date" },
      "participants": { "type": "integer", "minimum": 1 },
      "status": { "type": "string", "enum": ["Not Started", "Ongoing", "Completed"] }
   },
   "required": ["trialId", "title", "startDate", "status"],
   "additionalProperties": false
}
