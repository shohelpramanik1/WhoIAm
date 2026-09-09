# WHO I AM - Deployment Guide

## Production Deployment

This guide covers deploying Who I Am to production environments.

## Prerequisites

- .NET 9 runtime
- Node.js 18+
- SQL Server 2019+
- Redis
- Docker & Docker Compose (recommended)

## Environment Setup

### 1. Database Configuration

Update connection string in `appsettings.Production.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-sql-server;Database=WhoIAm;User Id=sa;Password=<secure-password>;Encrypt=true;TrustServerCertificate=false;"
  }
}
```

### 2. JWT Configuration

Generate a strong JWT key:

```bash
openssl rand -base64 32
```

Update `appsettings.Production.json`:

```json
{
  "Jwt": {
    "Key": "<generated-key>",
    "Issuer": "whoiam-api",
    "Audience": "whoiam-app",
    "ExpiryMinutes": 15,
    "RefreshTokenExpiryDays": 7
  }
}
```

### 3. Redis Configuration

```json
{
  "Redis": {
    "ConnectionString": "prod-redis-host:6379"
  }
}
```

## Docker Deployment

### Build Images

```bash
# Build all images
docker-compose -f docker-compose.yml build

# Push to registry
docker tag whoiam-api:latest myregistry.azurecr.io/whoiam-api:latest
docker push myregistry.azurecr.io/whoiam-api:latest
```

### Deploy

```bash
docker-compose -f docker-compose.yml up -d
```

## Health Checks

Verify deployment:

```bash
# API Health
curl https://api.whoiam.com/health

# Database
sqlcmd -S server -U user -P password -Q "SELECT 1"

# Redis
redis-cli ping
```

## Security Checklist

- [ ] HTTPS enabled
- [ ] JWT key secured
- [ ] Database password strong
- [ ] Redis password set
- [ ] CORS configured correctly
- [ ] Rate limiting enabled
- [ ] Logging configured
- [ ] Backups scheduled
- [ ] Secrets not in code
- [ ] Dependencies updated

## Monitoring

### Logs

```bash
# Docker
docker-compose logs -f api

# File-based
tail -f /var/log/whoiam/log-*.txt
```

### Performance

- Monitor database query performance
- Track API response times
- Monitor Redis memory usage
- Alert on error rates

## Backup & Recovery

### Database Backup

```sql
BACKUP DATABASE WhoIAm
TO DISK = '/var/opt/mssql/backup/WhoIAm_backup.bak'
```

### Restore

```sql
RESTORE DATABASE WhoIAm
FROM DISK = '/var/opt/mssql/backup/WhoIAm_backup.bak'
```

## Troubleshooting

### API Won't Start

1. Check database connection
2. Verify migrations applied: `dotnet ef database update`
3. Check logs: `docker-compose logs api`

### Database Connection Issues

1. Verify connection string
2. Check SQL Server is running
3. Verify network connectivity
4. Check credentials

### Performance Issues

1. Monitor database indexes
2. Check query performance
3. Verify Redis is working
4. Check API logs for slow requests

## Scaling

### Horizontal Scaling

- Deploy multiple API instances behind load balancer
- Use shared Redis instance
- Use replicated database

### Database Optimization

- Add appropriate indexes
- Archive old data
- Optimize queries
- Consider caching strategies
