using FluentValidation;

using QingFa.EShop.Application.Features.AccountManagements.Errors;

namespace QingFa.EShop.Application.Features.AccountManagements.ResetPassword
{
    internal class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage(ValidationMessages.ValidEmailRequired);

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage(ValidationMessages.ConfirmCodeRequired);

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(ValidationMessages.NewPasswordRequired);
        }
    }
}
