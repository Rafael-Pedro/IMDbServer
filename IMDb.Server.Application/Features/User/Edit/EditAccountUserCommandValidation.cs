using FluentValidation;

namespace IMDb.Server.Application.Features.User.Edit;

public class EditAccountUserCommandValidation : AbstractValidator<EditAccountUserCommand>
{
    public EditAccountUserCommandValidation()
    {
        When(eauc => eauc is not null, () => RuleFor(eauc => eauc.Username).MinimumLength(6).MaximumLength(35));
        When(eauc => eauc is not null, () => RuleFor(eauc => eauc.Password).MinimumLength(6));
        When(eauc => eauc is not null, () => RuleFor(eauc => eauc.Email).EmailAddress().MaximumLength(256));
    }
}
