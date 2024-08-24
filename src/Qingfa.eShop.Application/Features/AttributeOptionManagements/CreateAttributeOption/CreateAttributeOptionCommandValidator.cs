using FluentValidation;

namespace QingFa.EShop.Application.Features.AttributeOptionManagements.CreateAttributeOption
{
    internal class CreateAttributeOptionCommandValidator : AbstractValidator<CreateAttributeOptionCommand>
    {
        public CreateAttributeOptionCommandValidator()
        {
            RuleFor(x => x.OptionValue)
                .NotEmpty().WithMessage("Option value cannot be null or empty.")
                .Length(1, 100).WithMessage("Option value must be between 1 and 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sort order must be a non-negative integer.");

            RuleFor(x => x.ProductAttributeId)
                .NotEmpty().WithMessage("Product attribute ID cannot be empty.");
        }
    }
}
