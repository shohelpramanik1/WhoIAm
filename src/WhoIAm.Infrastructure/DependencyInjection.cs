using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using WhoIAm.Application.Interfaces;
using WhoIAm.Infrastructure.Data;
using WhoIAm.Infrastructure.Repositories;
using WhoIAm.Infrastructure.Services;

namespace WhoIAm.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not configured");
        
        services.AddDbContext<WhoIAmDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("WhoIAm.Infrastructure"))
        );

        // Redis
        var redisConnectionString = configuration["Redis:ConnectionString"] ?? "localhost:6379";
        var redis = ConnectionMultiplexer.Connect(redisConnectionString);
        services.AddSingleton(redis);
        services.AddScoped<ICacheService, RedisCacheService>();

        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<UserRepository>();
        services.AddScoped<VirtualIdentityRepository>();
        services.AddScoped<PostRepository>();
        services.AddScoped<CommunityRepository>();
        services.AddScoped<MoodRepository>();
        services.AddScoped<JournalRepository>();
        services.AddScoped<EnemyRepository>();
        services.AddScoped<MissionRepository>();

        // Services
        services.AddScoped<ITokenService, JwtTokenService>();
        
        var environment = configuration["ASPNETCORE_ENVIRONMENT"] ?? "Development";
        if (environment == "Development")
        {
            services.AddScoped<IEmailService, DevelopmentEmailService>();
        }
        // In production, inject SmtpEmailService

        return services;
    }
}
