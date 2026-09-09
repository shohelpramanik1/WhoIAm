using WhoIAm.Application.Interfaces;

namespace WhoIAm.Infrastructure.Services;

public class DevelopmentEmailService : IEmailService
{
    private readonly ILogger<DevelopmentEmailService> _logger;

    public DevelopmentEmailService(ILogger<DevelopmentEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailVerificationAsync(string email, string token)
    {
        _logger.LogInformation($"[Development] Email verification sent to {email}");
        _logger.LogInformation($"Verification token: {token}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetAsync(string email, string token)
    {
        _logger.LogInformation($"[Development] Password reset sent to {email}");
        _logger.LogInformation($"Reset token: {token}");
        return Task.CompletedTask;
    }

    public Task SendWelcomeEmailAsync(string email, string username)
    {
        _logger.LogInformation($"[Development] Welcome email sent to {email} for user {username}");
        return Task.CompletedTask;
    }

    public Task SendNotificationEmailAsync(string email, string subject, string content)
    {
        _logger.LogInformation($"[Development] Notification email sent to {email}");
        _logger.LogInformation($"Subject: {subject}");
        _logger.LogInformation($"Content: {content}");
        return Task.CompletedTask;
    }
}
