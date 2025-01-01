# Appointment Management System

This is a simple Appointment Management System built with ASP.NET Core and SQL Server. The system provides APIs for user registration, login, and appointment booking functionalities.

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 or later
- Postman (optional, for API testing)

## Getting Started

### Setting Up the Database

Run the following SQL script to create the required database and tables:

```sql
CREATE DATABASE AppointmentManagementDB;
GO

USE AppointmentManagementDB;
GO

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL
);
GO

CREATE TABLE Doctors (
    DoctorId INT IDENTITY(1,1) PRIMARY KEY,
    DoctorName NVARCHAR(200) NOT NULL
);
GO

CREATE TABLE Appointments (
    AppointmentId INT IDENTITY(1,1) PRIMARY KEY,
    PatientName NVARCHAR(200) NOT NULL,
    PatientContactInfo NVARCHAR(200) NOT NULL,
    AppointmentDateTime DATETIME NOT NULL CHECK (AppointmentDateTime > GETDATE()),
    DoctorId INT NOT NULL,
    FOREIGN KEY (DoctorId) REFERENCES Doctors(DoctorId) ON DELETE CASCADE
);
GO

INSERT INTO Doctors (DoctorName)
VALUES 
('Dr. John Doe'),
('Dr. Abdullah'),
('Dr. NasirUddin');
```

### Configuring the Application

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```

2. Open the project in Visual Studio.

3. Update the `appsettings.json` file with your database connection string and JWT settings:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=AppointmentManagementDB;Trusted_Connection=True;"
     },
     "JwtSettings": {
       "SecretKey": "4a68beeb-ed79-4604-b632-57291c628e78",
       "Issuer": "YourIssuer",
       "Audience": "YourAudience",
       "ExpirationInMinutes": 60
     }
   }
   ```

4. Restore the required packages:
   ```bash
   dotnet restore
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

The application will start, and the API will be available at `https://localhost:<port>`.

## API Endpoints

### User Endpoints

- **Register**: `POST /api/user/register`
- **Login**: `POST /api/user/login`

### Appointment Endpoints

- **Get All Appointments**: `GET /api/appointment`
- **Create Appointment**: `POST /api/appointment`

### Doctor Endpoints

- **Get All Doctors**: `GET /api/doctor`

## Swagger Integration

### Accessing Swagger

Once the application is running, Swagger can be accessed at `https://localhost:<port>/swagger`.

### Using Swagger for Authentication

1. Navigate to the Swagger UI.
2. Use the `POST /api/user/login` endpoint to generate a token by providing valid credentials.
3. Copy the token from the response.
4. Click the **Authorize** button in the Swagger UI.
5. Paste the token in the following format: `<your-token>`.
6. After authorizing, you can use the authenticated endpoints like `POST /api/appointment`.

## Middleware

- **Global Exception Handling Middleware**: Handles exceptions and provides consistent error responses.

## Error Handling

The application uses a global error-handling middleware to catch and format exceptions consistently.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Acknowledgments

- .NET Core Documentation
- SQL Server Documentation
