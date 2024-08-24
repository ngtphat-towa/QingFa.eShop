using FluentValidation;

namespace QingFa.EShop.Application.Features.AttributeManagements.CreateAttribute
{
    internal class CreateProductAttributeCommandValidator : AbstractValidator<CreateProductAttributeCommand>
    {
        public CreateProductAttributeCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(command => command.AttributeCode)
                .NotEmpty().WithMessage("Attribute code cannot be empty.")
                .MaximumLength(100).WithMessage("Attribute code cannot exceed 100 characters.");

            RuleFor(command => command.AttributeGroupId)
                .NotEmpty().WithMessage("Attribute Group ID must be provided.");

            RuleFor(command => command.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(command => command.SortOrder)
                .GreaterThanOrEqualTo(0).WithMessage("Sort order must be a non-negative integer.");
        }
    }
}
