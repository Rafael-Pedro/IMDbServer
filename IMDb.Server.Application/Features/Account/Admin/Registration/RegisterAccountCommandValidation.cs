using FluentValidation;

namespace IMDb.Server.Application.Features.Account.Admin.Registration;

public class RegisterAccountCommandValidation : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidation()
    {
        RuleFor(rf => rf.Login).NotEmpty().MinimumLength(6);
        RuleFor(rf => rf.Password).NotEmpty().MinimumLength(6);
    }
}
