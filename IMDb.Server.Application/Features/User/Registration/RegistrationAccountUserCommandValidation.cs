using FluentValidation;

namespace IMDb.Server.Application.Features.User.Registration;

public class RegistrationAccountUserCommandValidation : AbstractValidator<RegistrationAccountUserCommand>
{
    public RegistrationAccountUserCommandValidation()
    {
    }
}
