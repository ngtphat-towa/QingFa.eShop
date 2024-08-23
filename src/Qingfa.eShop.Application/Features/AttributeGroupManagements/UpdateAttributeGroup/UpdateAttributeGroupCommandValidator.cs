using FluentValidation;

namespace QingFa.EShop.Application.Features.AttributeGroupManagements.UpdateAttributeGroup
{
    internal class UpdateAttributeGroupCommandValidator : AbstractValidator<UpdateAttributeGroupCommand>
    {
        public UpdateAttributeGroupCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");
        }
    }
}
