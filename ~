# Database Setup Instructions

## Prerequisites
- SQL Server LocalDB or SQL Server Express
- .NET 6.0 or later

## Setup Steps

1. **Install LocalDB** (if not already installed)
   ```bash
   # Download from Microsoft website or install via Visual Studio


2. **Start LocalDB**
   ```bash
   sqllocaldb start mssqllocaldb


3. **Update Connection String** Update appsettings.json with your local connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EPC_Db;Trusted_Connection=true;MultipleActiveResultSets=true"
     }
   }


4. **Run Database Migrations**
   ```bash
   dotnet ef database update


5. **Run the Application**
   ```bash
   dotnet run
