using FluentValidation;

namespace IMDb.Server.Application.Features.Account.User.Login;

public class LoginAccountUserValidation : AbstractValidator<LoginAccountUserCommand>
{
    public LoginAccountUserValidation()
    {
        RuleFor(lauv => lauv.Username).NotEmpty().MinimumLength(6);
        RuleFor(lauv => lauv.Password).NotEmpty().MinimumLength(6);
    }
}
