using FluentValidation;
using WhoIAm.Application.DTOs.Post;

namespace WhoIAm.Application.Validators.Post;

public class CreatePostValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Post content is required")
            .MaximumLength(5000).WithMessage("Post content must not exceed 5000 characters");

        RuleFor(x => x.Visibility)
            .Must(x => new[] { "Public", "Followers", "Community", "Private" }.Contains(x))
            .WithMessage("Visibility must be Public, Followers, Community, or Private");

        RuleFor(x => x.Type)
            .Must(x => new[] { "Text", "Image", "Video", "Audio", "Poll", "Confession", "Story" }.Contains(x))
            .WithMessage("Post type is invalid");
    }
}
