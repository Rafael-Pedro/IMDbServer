using FluentValidation;

namespace IMDb.Server.Application.Features.Account.Adm.Registration;

public class RegisterAccountCommandValidation : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidation()
    {
        RuleFor(rac => rac.Username).NotEmpty().MinimumLength(6);
        RuleFor(rac => rac.Email).NotEmpty().MaximumLength(256);
        RuleFor(rac => rac.Password).NotEmpty().MinimumLength(6);
    }
}
