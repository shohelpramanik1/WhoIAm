# WHO I AM - Development Setup Guide

## Prerequisites

- .NET 9 SDK or higher ([Download](https://dotnet.microsoft.com/download))
- Node.js 18+ and npm ([Download](https://nodejs.org/))
- SQL Server 2019+ or Docker
- Git

## Option 1: Local Development (Without Docker)

### 1. Clone the Repository

```bash
git clone https://github.com/shohelpramanik1/WhoIAm.git
cd WhoIAm
```

### 2. Set Up SQL Server

#### Option A: Local SQL Server

Create a database:

```sql
CREATE DATABASE WhoIAm;
USE WhoIAm;
```

#### Option B: SQL Server in Docker

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourPassword123!" \
  -p 1433:1433 --name whoiam-mssql \
  mcr.microsoft.com/mssql/server:2022-latest
```

### 3. Set Up Redis (Optional but Recommended)

```bash
# Using Docker
docker run -p 6379:6379 --name whoiam-redis redis:7-alpine

# Or install locally on macOS
brew install redis
redis-server

# Or on Ubuntu
sudo apt-get install redis-server
redis-server
```

### 4. Configure Environment

Create `src/WhoIAm.Api/appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=WhoIAm;User Id=sa;Password=YourPassword123!;Encrypt=false;"
  },
  "Jwt": {
    "Key": "this-is-a-secret-key-that-must-be-at-least-32-characters-long-for-jwt",
    "Issuer": "whoiam-api",
    "Audience": "whoiam-app",
    "ExpiryMinutes": 15,
    "RefreshTokenExpiryDays": 7
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  },
  "Email": {
    "Provider": "Development",
    "SmtpServer": "localhost",
    "SmtpPort": 1025,
    "FromEmail": "noreply@whoiam.local",
    "FromName": "Who I Am"
  }
}
```

### 5. Install Backend Dependencies

```bash
cd src/WhoIAm.Api
dotnet restore
```

### 6. Run Database Migrations

```bash
cd src/WhoIAm.Api
dotnet ef database update
```

This will:
- Create the database schema
- Seed initial data (communities, templates, etc.)

### 7. Run the API

```bash
cd src/WhoIAm.Api
dotnet run
```

API will be available at: `https://localhost:5001`

**Swagger UI**: `https://localhost:5001/swagger`

### 8. Install Frontend Dependencies

In a new terminal:

```bash
cd src/WhoIAm.Web
npm install
```

### 9. Run the Frontend

```bash
cd src/WhoIAm.Web
npm run dev
```

Web app will be available at: `http://localhost:5173`

## Option 2: Docker Development

### 1. Clone Repository

```bash
git clone https://github.com/shohelpramanik1/WhoIAm.git
cd WhoIAm
```

### 2. Start Containers

```bash
docker-compose up -d
```

This will start:
- **SQL Server** at `localhost:1433`
- **Redis** at `localhost:6379`
- **API** at `https://localhost:5001`
- **Web** at `http://localhost:3000`

### 3. View Logs

```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f api
```

### 4. Stop Containers

```bash
docker-compose down
```

## Verify Installation

### Backend Health

```bash
curl https://localhost:5001/health
```

Expected response:
```json
{"status": "Healthy"}
```

### Frontend Health

Open browser: `http://localhost:5173`

## Database Migrations

### Create a New Migration

```bash
cd src/WhoIAm.Api
dotnet ef migrations add YourMigrationName --project ../WhoIAm.Infrastructure
```

### Apply Migrations

```bash
cd src/WhoIAm.Api
dotnet ef database update
```

### Revert Last Migration

```bash
cd src/WhoIAm.Api
dotnet ef database update PreviousMigrationName
```

## Running Tests

### Unit Tests

```bash
dotnet test tests/WhoIAm.UnitTests/
```

### Integration Tests

```bash
dotnet test tests/WhoIAm.IntegrationTests/
```

### All Tests with Coverage

```bash
dotnet test /p:CollectCoverage=true
```

## Common Issues

### SQL Server Connection Error

**Error**: "Cannot connect to SQL Server"

**Solution**:
- Verify SQL Server is running
- Check connection string in `appsettings.Development.json`
- For Docker: ensure the container is healthy: `docker ps`

### Port Already in Use

**Error**: "Port 5001 is already in use"

**Solution**:
```bash
# Find process using port
lsof -i :5001  # macOS/Linux
netstat -ano | findstr :5001  # Windows

# Kill process
kill -9 <PID>  # macOS/Linux
taskkill /PID <PID> /F  # Windows
```

### Certificate Issues (HTTPS)

**Error**: "Unable to configure HTTPS endpoint"

**Solution**:
```bash
# Create self-signed certificate
dotnet dev-certs https --clean
dotnet dev-certs https
```

### Node Modules Issues

**Error**: npm install fails

**Solution**:
```bash
cd src/WhoIAm.Web
rm -rf node_modules package-lock.json
npm install
```

## IDE Setup

### Visual Studio 2022

1. Open `WhoIAm.sln`
2. Set `WhoIAm.Api` as startup project
3. Press F5 to run

### Visual Studio Code

Recommended extensions:
- C# (ms-dotnettools.csharp)
- C# Dev Kit (ms-dotnettools.csdevkit)
- REST Client (humao.rest-client)
- Thunder Client or Postman for API testing

### JetBrains Rider

1. Open the project folder
2. Configure run configuration for `WhoIAm.Api`
3. Click Run

## API Testing

### Using Swagger UI

1. Navigate to `https://localhost:5001/swagger`
2. Authorize with token
3. Test endpoints

### Using Postman

1. Import the API collection from `docs/postman/collection.json`
2. Configure environment variables
3. Test endpoints

### Using curl

```bash
# Register
curl -X POST https://localhost:5001/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "username": "username",
    "password": "Password123!",
    "dateOfBirth": "2000-01-01",
    "country": "US"
  }'

# Login
curl -X POST https://localhost:5001/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123!"
  }'
```

## Seed Data

The application automatically seeds initial data on first run:

- 5 sample users
- 15 communities
- 10 enemy templates
- 20+ reflection templates
- 20+ mission templates

To reseed:

```bash
cd src/WhoIAm.Api
dotnet ef database drop
dotnet ef database update
```

## Development Workflow

1. **Backend Changes**:
   - Make code changes in `src/WhoIAm.*` projects
   - Run tests: `dotnet test`
   - Migrations: `dotnet ef migrations add YourMigration`
   - API will hot-reload with dotnet watch

2. **Frontend Changes**:
   - Make changes in `src/WhoIAm.Web/src`
   - Vite hot-reloads automatically
   - Run tests: `npm test`

3. **Database Changes**:
   - Create migration: `dotnet ef migrations add YourMigration`
   - Update database: `dotnet ef database update`

## Performance Tips

- Enable query caching with Redis
- Use pagination for large datasets
- Enable GZIP compression
- Use CDN for static assets
- Enable lazy loading for images

## Production Deployment

See `docs/DEPLOYMENT.md` for production setup, security checklist, and deployment procedures.

## Support

For issues:
1. Check the troubleshooting section above
2. Review GitHub Issues
3. Create a new issue with detailed error messages

## Next Steps

1. Explore the API at `https://localhost:5001/swagger`
2. Check out the frontend at `http://localhost:5173`
3. Read the documentation in `docs/`
4. Start implementing features following the development phases
