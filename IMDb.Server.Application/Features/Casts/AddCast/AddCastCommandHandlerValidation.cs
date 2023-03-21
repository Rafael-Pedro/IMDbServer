using FluentValidation;

namespace IMDb.Server.Application.Features.Casts.AddCast;

public class AddCastCommandHandlerValidation : AbstractValidator<AddCastCommand>
{
    public AddCastCommandHandlerValidation()
    {
        RuleFor(rf => rf.Name).MaximumLength(200);
        RuleFor(rf => rf.Description).MaximumLength(400);
    }
}
