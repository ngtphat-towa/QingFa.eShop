using FluentValidation;

using QingFa.EShop.Application.Features.AccountManagements.Errors;

namespace QingFa.EShop.Application.Features.AccountManagements.ConfirmEmail
{
    internal class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage(ValidationMessages.UserNameRequired);

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(ValidationMessages.ConfirmCodeRequired);

            RuleFor(x => x.ChangeEmail)
                .NotEmpty()
                .EmailAddress()
                .WithMessage(ValidationMessages.ValidEmailRequired);
        }
    }
}
