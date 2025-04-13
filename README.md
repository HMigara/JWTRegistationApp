# JWTRegistationApp

# User Authentication Web Application

A simple web application with user registration and authentication using JWT tokens.

## Features

- User Registration (username, password, document upload, date field)
- User Authentication (JWT tokens)
- Secure login/logout functionality
- User profile display

## Technology Stack

- **Backend**: ASP.NET Core Web API
- **Database**: MySQL
- **Frontend**: HTML, CSS, JavaScript
- **Authentication**: JWT (JSON Web Tokens)

## Prerequisites

- .NET 7.0 SDK or later
- MySQL Server 8.0 or later
- Visual Studio 2022 or VS Code

## Setup Instructions

### 1. Database Setup

Create a new MySQL database:

```sql
CREATE DATABASE userauth;
```

Update the connection string in `appsettings.json` to match your MySQL setup:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=userauth;user=YOUR_USERNAME;password=YOUR_PASSWORD"
}
```

### 2. JWT Secret Key

Generate a strong secret key and update it in the `appsettings.json` file:

```json
"Jwt": {
  "Key": "YourSecretKeyHereMakeItAtLeast32CharactersLong"
}
```

### 3. Project Setup

Clone this repository or copy the files to your local machine.

Install required NuGet packages:

```
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package BCrypt.Net-Next
```

### 4. Database Migration

Run the following commands to create the database schema:

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 5. Build and Run

Build the application:

```
dotnet build
```

Run the application:

```
dotnet run
```

The application should now be running at `https://localhost:7xxx` (the exact port will be displayed in the console).

## Security Considerations

- The application uses HTTPS for secure data transfer
- Passwords are hashed using BCrypt before storing
- JWT tokens are used for authentication
- File uploads are stored securely with randomized names

## Testing

Basic tests are included in the project. Run them using:

```
dotnet test
```
