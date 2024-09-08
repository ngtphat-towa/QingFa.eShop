using FluentValidation;

using QingFa.EShop.Application.Features.AccountManagements.Errors;

namespace QingFa.EShop.Application.Features.AccountManagements.LogInAccount
{
    internal class LoginRequestValidator : AbstractValidator<LogInAccountRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.UserName) && string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage(ValidationMessages.EmailRequired);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.Email) && string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage(ValidationMessages.UsernameRequired);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.Email) && string.IsNullOrEmpty(x.UserName))
                .WithMessage(ValidationMessages.PhoneNumberRequired);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ValidationMessages.PasswordRequired);
        }
    }
}
