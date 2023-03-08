using FluentValidation;

namespace IMDb.Server.Application.Features.Account.Adm.Login;

public class LoginAccountCommandValidation : AbstractValidator<LoginAccountCommand>
{
    public LoginAccountCommandValidation()
    {
        RuleFor(lacv => lacv.Username).MinimumLength(6);
        RuleFor(lacv => lacv.Password).MinimumLength(6);
    }
}
