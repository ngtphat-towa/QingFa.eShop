using FluentValidation;

using QingFa.EShop.Application.Features.AccountManagements.Errors;

namespace QingFa.EShop.Application.Features.AccountManagements.ResendConfirmationEmail
{
    internal class ResendConfirmationEmailRequestValidator : AbstractValidator<ResendConfirmationEmailRequest>
    {
        public ResendConfirmationEmailRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage(ValidationMessages.ValidEmailRequired);
        }
    }
}
