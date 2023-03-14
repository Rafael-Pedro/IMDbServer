using FluentValidation;

namespace IMDb.Server.Application.Features.Account.Adm.Edit
{
    public class EditAccountAdminCommandValidation : AbstractValidator<EditAccountAdminCommand>
    {
        public EditAccountAdminCommandValidation()
        {
            When(rf => rf is not null, () => RuleFor(rf => rf.Username).MinimumLength(6).MaximumLength(35));
            When(rf => rf is not null, () => RuleFor(rf => rf.Password).MinimumLength(6));
            When(rf => rf is not null, () => RuleFor(rf => rf.Email).EmailAddress().MaximumLength(256));
        }
    }
}
