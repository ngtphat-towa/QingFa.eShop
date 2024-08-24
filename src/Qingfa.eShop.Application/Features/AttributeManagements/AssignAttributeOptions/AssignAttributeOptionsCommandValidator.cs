using FluentValidation;

namespace QingFa.EShop.Application.Features.AttributeManagements.AssignAttributeOptions
{
    internal class AssignAttributeOptionsCommandValidator : AbstractValidator<AssignAttributeOptionsCommand>
    {
        public AssignAttributeOptionsCommandValidator()
        {
            RuleFor(x => x.ProductAttributeId)
                .NotEmpty().WithMessage("Product Attribute ID must not be empty.");

            RuleFor(x => x.AttributeOptionIds)
                .NotNull().WithMessage("Attribute Option IDs must not be null.")
                .Must(ids => ids.Any()).WithMessage("At least one Attribute Option ID must be provided.");

            RuleForEach(x => x.AttributeOptionIds)
                .NotEmpty().WithMessage("Attribute Option ID must not be empty.");
        }
    }
}
