using FluentValidation;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.CreateAttributeGroup
{
    internal class CreateAttributeGroupCommandValidator : AbstractValidator<CreateAttributeGroupCommand>
    {
        public CreateAttributeGroupCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
        }
    }
}
