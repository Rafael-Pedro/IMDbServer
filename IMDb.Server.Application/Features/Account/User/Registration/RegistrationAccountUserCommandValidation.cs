using FluentValidation;

namespace IMDb.Server.Application.Features.Account.User.Registration;

public class RegistrationAccountUserCommandValidation : AbstractValidator<RegistrationAccountUserCommand>
{
    public RegistrationAccountUserCommandValidation()
    {
    }
}
