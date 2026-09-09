using Microsoft.EntityFrameworkCore;
using WhoIAm.Infrastructure.Data;
using Xunit;
using WhoIAm.Domain.Entities;

namespace WhoIAm.IntegrationTests.Data;

public class RepositoryTests
{
    private WhoIAmDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<WhoIAmDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new WhoIAmDbContext(options);
    }

    [Fact]
    public async Task AddUser_ShouldPersistToDatabase()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@example.com",
            Username = "testuser",
            PasswordHash = "hashed_password",
            DateOfBirth = DateTime.Now.AddYears(-25),
            Country = "US",
            Active = true
        };

        // Act
        context.Users.Add(user);
        await context.SaveChangesAsync();

        // Assert
        var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "test@example.com");
        Assert.NotNull(savedUser);
        Assert.Equal("testuser", savedUser.Username);
    }

    [Fact]
    public async Task CreateVirtualIdentity_ShouldLinkToUser()
    {
        // Arrange
        using var context = GetInMemoryContext();
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            Email = "test@example.com",
            Username = "testuser",
            PasswordHash = "hashed_password",
            DateOfBirth = DateTime.Now.AddYears(-25),
            Country = "US",
            Active = true
        };
        context.Users.Add(user);

        var identity = new VirtualIdentity
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            DisplayName = "Raven",
            Username = "raven123",
            IsDefault = true
        };
        context.VirtualIdentities.Add(identity);
        await context.SaveChangesAsync();

        // Act
        var retrievedUser = await context.Users
            .Include(u => u.VirtualIdentities)
            .FirstOrDefaultAsync(u => u.Id == userId);

        // Assert
        Assert.NotNull(retrievedUser);
        Assert.Single(retrievedUser.VirtualIdentities);
        Assert.Equal("Raven", retrievedUser.VirtualIdentities.First().DisplayName);
    }
}
