using FluentValidation;

using QingFa.EShop.Application.Features.AccountManagements.Errors;

namespace QingFa.EShop.Application.Features.AccountManagements.RegisterAccount
{
    internal class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public RegistrationRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(ValidationMessages.FirstNameRequired);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(ValidationMessages.LastNameRequired);

            RuleFor(x => x.Email)
            .NotEmpty()
                .EmailAddress()
                .WithMessage(ValidationMessages.ValidEmailRequired);

            RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
                .WithMessage(ValidationMessages.PasswordMinLength);

            RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
                .WithMessage(ValidationMessages.PasswordMismatch);

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?\d{10,15}$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage(ValidationMessages.InvalidPhoneNumber);

            RuleFor(x => x.ShippingAddress)
            .NotNull()
                .WithMessage(ValidationMessages.AddressRequired);

            RuleFor(x => x.BillingAddress)
            .NotNull()
                .WithMessage(ValidationMessages.AddressRequired);
        }
    }
}
