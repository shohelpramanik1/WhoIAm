using FluentValidation;
using WhoIAm.Application.DTOs.VirtualIdentity;

namespace WhoIAm.Application.Validators.VirtualIdentity;

public class CreateVirtualIdentityValidator : AbstractValidator<CreateVirtualIdentityRequest>
{
    public CreateVirtualIdentityValidator()
    {
        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Display name is required")
            .Length(2, 50).WithMessage("Display name must be between 2 and 50 characters");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 32).WithMessage("Username must be between 3 and 32 characters")
            .Matches(@"^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores, and hyphens");

        RuleFor(x => x.Bio)
            .MaximumLength(500).WithMessage("Bio must not exceed 500 characters");

        RuleFor(x => x.QuietudeLevel)
            .InclusiveBetween(1, 10).WithMessage("Quietude level must be between 1 and 10");

        RuleFor(x => x.CalmLevel)
            .InclusiveBetween(1, 10).WithMessage("Calm level must be between 1 and 10");

        RuleFor(x => x.SeriousnessLevel)
            .InclusiveBetween(1, 10).WithMessage("Seriousness level must be between 1 and 10");

        RuleFor(x => x.IntroversionLevel)
            .InclusiveBetween(1, 10).WithMessage("Introversion level must be between 1 and 10");

        RuleFor(x => x.TraditionalismLevel)
            .InclusiveBetween(1, 10).WithMessage("Traditionalism level must be between 1 and 10");
    }
}
