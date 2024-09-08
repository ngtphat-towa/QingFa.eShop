using FluentValidation;

using QingFa.EShop.Application.Features.AccountManagements.Errors;

namespace QingFa.EShop.Application.Features.AccountManagements.UpdateUserInfo
{
    internal class UpdateUserInfoRequestValidator : AbstractValidator<UpdateUserInfoRequest>
    {
        public UpdateUserInfoRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage(ValidationMessages.FirstNameRequired);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage(ValidationMessages.LastNameRequired);

            RuleFor(x => x.Email)
                .NotEmpty()
                .When(x => string.IsNullOrEmpty(x.PhoneNumber) && string.IsNullOrEmpty(x.UserName))
                .WithMessage(ValidationMessages.EmailRequired)
                .EmailAddress()
                .WithMessage(ValidationMessages.ValidEmailRequired);

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
