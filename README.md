# WHO I AM

**Be the person you can't be anywhere else.**

## Overview

Who I Am is a full-featured web application for alternative identity exploration, anonymous emotional expression, and healthy self-reflection.

### Tagline
**Express. Explore. Release. Return.**

## Product Philosophy

**REAL LIFE → EMOTION → EXPRESSION → CONNECTION → REFLECTION → RELEASE → REAL-LIFE ACTION**

## Technology Stack

### Backend
- ASP.NET Core 9/10 Web API
- C# 12
- Entity Framework Core
- ASP.NET Core Identity
- JWT Authentication
- SignalR for real-time communication

### Database
- Microsoft SQL Server
- Entity Framework Core migrations

### Frontend
- React 18+
- TypeScript
- Vite
- Tailwind CSS
- Zustand (state management)

### Infrastructure
- Redis (caching, rate limiting)
- Docker & Docker Compose
- Serilog (structured logging)
- FluentValidation
- AutoMapper

## Project Structure

```
WhoIAm/
├── src/
│   ├── WhoIAm.Api/                 # Main API application
│   ├── WhoIAm.Application/         # Application layer (services, DTOs)
│   ├── WhoIAm.Domain/              # Domain entities and interfaces
│   ├── WhoIAm.Infrastructure/      # Infrastructure (EF, repositories)
│   └── WhoIAm.Web/                 # React frontend
├── tests/
│   ├── WhoIAm.UnitTests/
│   ├── WhoIAm.IntegrationTests/
│   └── WhoIAm.E2ETests/
├── database/                        # SQL migrations and seed data
├── docker/                          # Docker configuration
├── docs/                            # Documentation
└── scripts/                         # Utility scripts
```

## Quick Start

### Prerequisites
- .NET 9 SDK or higher
- Node.js 18+
- SQL Server 2019+
- Docker & Docker Compose (optional)

### Backend Setup

```bash
cd src/WhoIAm.Api
dotnet restore
dotnet ef database update
dotnet run
```

API will be available at `https://localhost:5001`

### Frontend Setup

```bash
cd src/WhoIAm.Web
npm install
npm run dev
```

Web will be available at `http://localhost:5173`

### Docker Setup

```bash
docker-compose up -d
```

## Core Features

### Phase 1 - Foundation (MVP)
- ✅ User registration and login
- ✅ Email verification
- ✅ JWT authentication
- ✅ User profiles
- ✅ Logging and error handling

### Phase 2 - Virtual Identities
- 🔄 Create virtual identities ("My Other Me")
- 🔄 Identity Studio with customization
- 🔄 Identity switching
- 🔄 Privacy controls

### Phase 3 - Social Features
- 🔄 Posts and feed
- 🔄 Comments and reactions
- 🔄 Follow/unfollow
- 🔄 Search functionality

### Phase 4 - Communities
- 🔄 Create and join communities
- 🔄 Community moderation
- 🔄 Community posts and discussions

### Phase 5 - Emotional Features
- 🔄 Mood tracking
- 🔄 Dark Room (private journal)
- 🔄 Confessions
- 🔄 Burn It feature

### Phase 6 - Enemy System
- 🔄 Create symbolic enemies
- 🔄 Enemy Arena interactions
- 🔄 Reflection system

### Phase 7 - Real-Life Missions
- 🔄 Mission system
- 🔄 Real-life challenges
- 🔄 Progress tracking

### Phase 8 - Communication
- 🔄 Direct messaging
- 🔄 Real-time notifications
- 🔄 Anonymous chat

### Phase 9 - Safety & Moderation
- 🔄 Content reporting
- 🔄 Moderation dashboard
- 🔄 Account blocking/muting
- 🔄 Appeal system

### Phase 10 - Administration
- 🔄 Admin dashboard
- 🔄 User management
- 🔄 Analytics
- 🔄 Audit logging

### Phase 11 - Monetization
- 🔄 Premium subscriptions
- 🔄 Payment processing
- 🔄 Creator features

## API Documentation

Once the API is running, Swagger documentation is available at:
```
https://localhost:5001/swagger
```

## Database Migrations

Create a new migration:
```bash
cd src/WhoIAm.Infrastructure
dotnet ef migrations add MigrationName --project ../WhoIAm.Infrastructure --startup-project ../WhoIAm.Api
```

Apply migrations:
```bash
cd src/WhoIAm.Api
dotnet ef database update
```

## Running Tests

```bash
# Unit tests
dotnet test tests/WhoIAm.UnitTests/

# Integration tests
dotnet test tests/WhoIAm.IntegrationTests/

# All tests
dotnet test
```

## Environment Configuration

Create `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=WhoIAm;User Id=sa;Password=YourPassword123!;"
  },
  "Jwt": {
    "Key": "your-super-secret-key-that-is-long-enough",
    "Issuer": "whoiam-api",
    "Audience": "whoiam-app",
    "ExpiryMinutes": 15,
    "RefreshTokenExpiryDays": 7
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  }
}
```

## License

Proprietary - All rights reserved

## Support

For issues and feature requests, please use GitHub Issues.
