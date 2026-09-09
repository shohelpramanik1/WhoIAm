using Xunit;
using WhoIAm.Application.Validators.Auth;
using WhoIAm.Application.DTOs.Auth;

namespace WhoIAm.UnitTests.Validators;

public class AuthValidatorTests
{
    [Fact]
    public void RegisterRequest_WithValidData_ShouldPass()
    {
        // Arrange
        var validator = new RegisterRequestValidator();
        var request = new RegisterRequest
        {
            Email = "test@example.com",
            Username = "testuser",
            Password = "SecurePass123!",
            DateOfBirth = DateTime.Now.AddYears(-25),
            Country = "US",
            TermsAccepted = true
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void RegisterRequest_WithInvalidEmail_ShouldFail()
    {
        // Arrange
        var validator = new RegisterRequestValidator();
        var request = new RegisterRequest
        {
            Email = "invalid-email",
            Username = "testuser",
            Password = "SecurePass123!",
            DateOfBirth = DateTime.Now.AddYears(-25),
            Country = "US",
            TermsAccepted = true
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    [Fact]
    public void RegisterRequest_WithWeakPassword_ShouldFail()
    {
        // Arrange
        var validator = new RegisterRequestValidator();
        var request = new RegisterRequest
        {
            Email = "test@example.com",
            Username = "testuser",
            Password = "weak",
            DateOfBirth = DateTime.Now.AddYears(-25),
            Country = "US",
            TermsAccepted = true
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Password");
    }

    [Fact]
    public void RegisterRequest_UnderAgeUser_ShouldFail()
    {
        // Arrange
        var validator = new RegisterRequestValidator();
        var request = new RegisterRequest
        {
            Email = "test@example.com",
            Username = "testuser",
            Password = "SecurePass123!",
            DateOfBirth = DateTime.Now.AddYears(-16),
            Country = "US",
            TermsAccepted = true
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "DateOfBirth");
    }
}
