using FluentValidation;

namespace IMDb.Server.Application.Features.Casts.AddCast;

public class AddCastCommandValidation : AbstractValidator<AddCastCommand>
{
    public AddCastCommandValidation()
    {
        RuleFor(rf => rf.Name).MaximumLength(200);
        RuleFor(rf => rf.Description).MaximumLength(400);
    }
}
