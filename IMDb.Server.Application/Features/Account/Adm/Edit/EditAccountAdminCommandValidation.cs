using FluentValidation;

namespace IMDb.Server.Application.Features.Account.Adm.Edit
{
    public class EditAccountAdminCommandValidation : AbstractValidator<EditAccountAdminCommand>
    {
        public EditAccountAdminCommandValidation()
        {
            RuleFor(eaacv => eaacv.Username).MinimumLength(6);
            RuleFor(eaacv => eaacv.Password).MinimumLength(6);
            RuleFor(eaacv => eaacv.Email).EmailAddress().MaximumLength(6);
        }
    }
}
