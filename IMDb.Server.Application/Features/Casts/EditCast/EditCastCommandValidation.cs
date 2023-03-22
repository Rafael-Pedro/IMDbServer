using FluentValidation;

namespace IMDb.Server.Application.Features.Casts.EditCast;

public class EditCastCommandValidation : AbstractValidator<EditCastCommand>
{
    public EditCastCommandValidation()
    {
        RuleFor(rf => rf.Name).MaximumLength(200);
        RuleFor(rf => rf.Description).MaximumLength(400);
    }
}
