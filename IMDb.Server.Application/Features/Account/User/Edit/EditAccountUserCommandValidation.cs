using FluentValidation;

namespace IMDb.Server.Application.Features.Account.User.Edit;

public class EditAccountUserCommandValidation : AbstractValidator<EditAccountUserCommand>
{
    public EditAccountUserCommandValidation()
    {
        RuleFor(eaucv => eaucv.Username).MinimumLength(6);
        RuleFor(eaucv => eaucv.Password).MinimumLength(6);
        RuleFor(eaucv => eaucv.Email).EmailAddress().MinimumLength(6);
    }
}
