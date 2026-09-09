namespace WhoIAm.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailVerificationAsync(string email, string token);
    Task SendPasswordResetAsync(string email, string token);
    Task SendWelcomeEmailAsync(string email, string username);
    Task SendNotificationEmailAsync(string email, string subject, string content);
}
