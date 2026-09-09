# WHO I AM - Database Documentation

## Overview

Who I Am uses Microsoft SQL Server for persistent data storage with Entity Framework Core as the ORM.

## Schema

### Core Tables

#### Users
Stores user account information.

```sql
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Username NVARCHAR(32) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    DisplayName NVARCHAR(100),
    Bio NVARCHAR(500),
    Avatar NVARCHAR(MAX),
    DateOfBirth DATETIME2 NOT NULL,
    Country NVARCHAR(100) NOT NULL,
    AgeStatus INT NOT NULL DEFAULT 0,
    EmailVerified BIT NOT NULL DEFAULT 0,
    Active BIT NOT NULL DEFAULT 1,
    CurrentIdentityId UNIQUEIDENTIFIER,
    FollowerCount INT DEFAULT 0,
    FollowingCount INT DEFAULT 0,
    LastLoginAt DATETIME2,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2,
    DeletedAt DATETIME2,
    IsDeleted BIT NOT NULL DEFAULT 0
);
```

#### VirtualIdentities
Stores user's virtual personas.

```sql
CREATE TABLE VirtualIdentities (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    DisplayName NVARCHAR(50) NOT NULL,
    Username NVARCHAR(32) UNIQUE NOT NULL,
    Bio NVARCHAR(500),
    Avatar NVARCHAR(MAX),
    Visibility INT NOT NULL DEFAULT 0,
    FollowerCount INT DEFAULT 0,
    FollowingCount INT DEFAULT 0,
    PostCount INT DEFAULT 0,
    QuietudeLevel INT DEFAULT 5,
    CalmLevel INT DEFAULT 5,
    SeriousnessLevel INT DEFAULT 5,
    IntroversionLevel INT DEFAULT 5,
    TraditionalismLevel INT DEFAULT 5,
    CurrentMood NVARCHAR(50),
    IsDefault BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2,
    DeletedAt DATETIME2,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);
```

#### Posts
Stores user posts and expressions.

```sql
CREATE TABLE Posts (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    VirtualIdentityId UNIQUEIDENTIFIER,
    CommunityId UNIQUEIDENTIFIER,
    Content NVARCHAR(5000) NOT NULL,
    IsAnonymous BIT NOT NULL DEFAULT 0,
    Visibility INT NOT NULL DEFAULT 0,
    Type INT NOT NULL DEFAULT 0,
    CurrentMood NVARCHAR(50),
    ReactionCount INT DEFAULT 0,
    CommentCount INT DEFAULT 0,
    ShareCount INT DEFAULT 0,
    SaveCount INT DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2,
    DeletedAt DATETIME2,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (VirtualIdentityId) REFERENCES VirtualIdentities(Id) ON DELETE SET NULL,
    FOREIGN KEY (CommunityId) REFERENCES Communities(Id) ON DELETE SET NULL
);
```

#### Communities
Stores communities and discussion groups.

```sql
CREATE TABLE Communities (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Slug NVARCHAR(100) UNIQUE NOT NULL,
    Description NVARCHAR(1000) NOT NULL,
    Icon NVARCHAR(MAX),
    Banner NVARCHAR(MAX),
    Rules NVARCHAR(MAX),
    Type INT NOT NULL DEFAULT 0,
    Category INT NOT NULL DEFAULT 0,
    CreatedByUserId UNIQUEIDENTIFIER NOT NULL,
    MemberCount INT DEFAULT 0,
    PostCount INT DEFAULT 0,
    ExpiresAt DATETIME2,
    IsActive BIT NOT NULL DEFAULT 1,
    IsTemporary BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2,
    DeletedAt DATETIME2,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (CreatedByUserId) REFERENCES Users(Id) ON DELETE RESTRICT
);
```

#### Enemies
Stores symbolic enemies for the Enemy Arena feature.

```sql
CREATE TABLE Enemies (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    UserId UNIQUEIDENTIFIER NOT NULL,
    VirtualIdentityId UNIQUEIDENTIFIER NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(1000),
    Represents NVARCHAR(500) NOT NULL,
    Personality NVARCHAR(500),
    Avatar NVARCHAR(MAX),
    Category INT NOT NULL DEFAULT 0,
    IsTemplate BIT NOT NULL DEFAULT 0,
    SessionCount INT DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2,
    DeletedAt DATETIME2,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    FOREIGN KEY (VirtualIdentityId) REFERENCES VirtualIdentities(Id) ON DELETE CASCADE
);
```

## Migrations

View available migrations:

```bash
cd src/WhoIAm.Infrastructure
dotnet ef migrations list
```

Create new migration:

```bash
dotnet ef migrations add MigrationName --project ../WhoIAm.Infrastructure --startup-project ../WhoIAm.Api
```

Apply migrations:

```bash
cd src/WhoIAm.Api
dotnet ef database update
```

## Indexes

Key indexes for performance:

- Users: Email, Username
- VirtualIdentities: Username, UserId
- Posts: UserId, VirtualIdentityId, CreatedAt
- Communities: Slug, CreatedByUserId
- MoodEntries: UserId, VirtualIdentityId, CreatedAt
- JournalEntries: UserId, VirtualIdentityId, CreatedAt

## Soft Deletes

All entities support soft deletes via `IsDeleted` and `DeletedAt` fields. When querying, ensure to filter `WHERE IsDeleted = 0`.

## Relationships

### User Relationships
- One User → Many VirtualIdentities (1:N)
- One User → Many Posts (1:N)
- One User → Many Followers (N:M via UserFollower)
- One User → Many Communities (1:N creator)

### VirtualIdentity Relationships
- One VirtualIdentity → Many Posts (1:N)
- One VirtualIdentity → Many Enemies (1:N)
- One VirtualIdentity → Many Followers (N:M via VirtualIdentityFollower)
- One VirtualIdentity → Many MoodEntries (1:N)
- One VirtualIdentity → Many JournalEntries (1:N)

## Seed Data

See `Seeders/` directory for initial data setup.
