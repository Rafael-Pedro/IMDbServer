using FluentValidation;

namespace IMDb.Server.Application.Features.Account.Adm.Registration;

public class RegistrationAccountAdmCommandValidation : AbstractValidator<RegistrationAccountAdmCommand>
{
    public RegistrationAccountAdmCommandValidation()
    {
        RuleFor(rac => rac.Username).NotEmpty().MinimumLength(6);
        RuleFor(rac => rac.Email).NotEmpty().EmailAddress().MaximumLength(256);
        RuleFor(rac => rac.Password).NotEmpty().MinimumLength(6);
    }
}
