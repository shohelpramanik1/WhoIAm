# WHO I AM - Architecture Documentation

## System Overview

### Architecture Pattern

Who I Am uses a **Clean Architecture** approach with a Modular Monolith structure.

```
┌─────────────────────────────────────────────────────────────┐
│                    Web Application (React)                  │
│                  src/WhoIAm.Web/                            │
└────────────────────────┬────────────────────────────────────┘
                         │ HTTP/REST
                         │
┌────────────────────────▼────────────────────────────────────┐
│                     API Layer                               │
│              src/WhoIAm.Api/                                │
│  Controllers | Authentication | Middleware | Routing       │
└────────────────────────┬────────────────────────────────────┘
                         │ Services
                         │
┌────────────────────────▼────────────────────────────────────┐
│                Application Layer                            │
│         src/WhoIAm.Application/                             │
│  DTOs | Validators | Interfaces | Mappers                  │
└────────────────────────┬────────────────────────────────────┘
                         │ Repositories
                         │
┌────────────────────────▼────────────────────────────────────┐
│              Infrastructure Layer                           │
│          src/WhoIAm.Infrastructure/                         │
│  EF Core | Repositories | Services | Cache                 │
└────────────────────────┬────────────────────────────────────┘
                         │ Entities
                         │
┌────────────────────────▼────────────────────────────────────┐
│                  Domain Layer                               │
│             src/WhoIAm.Domain/                              │
│  Entities | Exceptions | Value Objects                      │
└─────────────────────────────────────────────────────────────┘
```

## Layer Responsibilities

### Domain Layer (WhoIAm.Domain)

Core business rules and entities.

**Responsibilities:**
- Define domain entities
- Aggregate roots
- Value objects
- Domain exceptions
- No external dependencies

**Key Files:**
- `Entities/` - Domain models
- `Exceptions/` - Domain exceptions
- `Common/` - Shared base classes

### Application Layer (WhoIAm.Application)

Application business logic and orchestration.

**Responsibilities:**
- DTOs for data transfer
- Validation rules
- Service interfaces
- Mappers
- Query/Command handlers

**Key Files:**
- `DTOs/` - Data transfer objects
- `Interfaces/` - Service contracts
- `Validators/` - FluentValidation rules

### Infrastructure Layer (WhoIAm.Infrastructure)

Implementation details and external services.

**Responsibilities:**
- Database access (EF Core)
- Repositories
- Cache service (Redis)
- Email service
- Token generation
- Dependency injection setup

**Key Files:**
- `Data/` - DbContext
- `Repositories/` - Data access
- `Services/` - Implementation of application interfaces

### API Layer (WhoIAm.Api)

HTTP interface and request handling.

**Responsibilities:**
- Controllers
- Routing
- Authentication middleware
- Error handling
- Startup configuration

**Key Files:**
- `Controllers/` - API endpoints
- `Program.cs` - Application startup
- `Services/` - API-specific services

### Web Layer (WhoIAm.Web)

User interface.

**Responsibilities:**
- React components
- State management (Zustand)
- API integration (Axios)
- Styling (Tailwind CSS)
- Routing

**Key Files:**
- `src/pages/` - Page components
- `src/store/` - Zustand stores
- `src/services/` - API services

## Data Flow

### Creating a Post

```
User Input
    ↓
React Component
    ↓
Axios API Call
    ↓
PostsController
    ↓
PostService
    ↓
PostRepository (Query)
    ↓
EF Core DbContext
    ↓
SQL Server Database
    ↓
Response → User
```

## Authentication Flow

```
Login Form
    ↓
AuthService.login()
    ↓
AuthController.login()
    ↓
AuthService.LoginAsync()
    ↓
UserRepository.GetByEmailAsync()
    ↓
Password Verification (BCrypt)
    ↓
JwtTokenService.GenerateAccessToken()
    ↓
Response with Token
    ↓
Store in Zustand + LocalStorage
    ↓
Include in Future Requests
```

## Key Services

### AuthService
Handles user registration, login, and token refresh.

### PostService
Manages post creation, retrieval, and interactions.

### CommunityService
Handles community operations.

### VirtualIdentityService
Manages identity creation and switching.

### JwtTokenService
Generates and validates JWT tokens.

### RedisCacheService
Handles caching operations.

## Database Design Principles

1. **Normalization**: Third normal form (3NF)
2. **Soft Deletes**: `IsDeleted` and `DeletedAt` columns
3. **Audit Trail**: `CreatedAt`, `UpdatedAt`, `DeletedAt`
4. **Referential Integrity**: Foreign keys with appropriate cascade options
5. **Indexing**: Strategic indexes for common queries

## Security Architecture

### Authentication
- JWT tokens for stateless authentication
- Access token: 15 minutes
- Refresh token: 7 days
- BCrypt password hashing

### Authorization
- Role-based access control (RBAC)
- Resource-level authorization
- Policy-based authorization

### Data Protection
- Encrypted connections (HTTPS/TLS)
- Sensitive data not logged
- SQL injection prevention (parameterized queries)
- XSS protection (output encoding)

## Caching Strategy

### Redis Cache
- User sessions
- Community data
- Hot posts
- User profiles
- Feed data

Cache Invalidation:
- Time-based (TTL)
- Event-based (post creation, etc.)
- Manual (admin operations)

## Error Handling

### API Response Format

```json
{
  "success": true,
  "data": {},
  "message": null,
  "errors": []
}
```

### Error Codes

- 400: Bad Request (validation)
- 401: Unauthorized
- 403: Forbidden
- 404: Not Found
- 409: Conflict
- 500: Server Error

## Testing Strategy

### Unit Tests
- Validators
- Business logic
- Services
- Utilities

### Integration Tests
- Database operations
- API endpoints
- Service combinations

### E2E Tests
- User journeys
- Complete workflows
- Cross-system interactions

## Performance Considerations

### Database
- Query optimization
- Index usage
- Connection pooling
- N+1 query prevention (eager loading)

### Caching
- Redis for hot data
- Distributed caching
- Cache warming

### API
- Pagination
- Response compression
- CDN for static files
- Rate limiting

## Scalability

### Horizontal Scaling
- Stateless API servers
- Load balancing
- Shared database
- Shared Redis

### Vertical Scaling
- Database optimization
- Query performance
- Caching strategies
- Resource allocation

## Future Enhancements

1. **Microservices**: Separate identity, social, messaging services
2. **Event Sourcing**: Audit trail and temporal queries
3. **CQRS**: Separate read and write models
4. **Search**: Elasticsearch for full-text search
5. **Real-time**: WebSocket for live notifications
6. **ML/AI**: Recommendation engine, content moderation
7. **Mobile**: Native mobile apps
8. **CDN**: Static content distribution
