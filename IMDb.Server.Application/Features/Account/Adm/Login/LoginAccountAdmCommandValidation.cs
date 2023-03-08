using FluentValidation;

namespace IMDb.Server.Application.Features.Account.Adm.Login;

public class LoginAccountAdmCommandValidation : AbstractValidator<LoginAccountAdmCommand>
{
    public LoginAccountAdmCommandValidation()
    {
        RuleFor(lacv => lacv.Username).MinimumLength(6);
        RuleFor(lacv => lacv.Password).MinimumLength(6);
    }
}
