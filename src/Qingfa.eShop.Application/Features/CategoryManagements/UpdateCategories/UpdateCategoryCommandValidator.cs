using FluentValidation;

using QingFa.EShop.Application.Features.Common.Extensions;

namespace QingFa.EShop.Application.Features.CategoryManagements.UpdateCategories
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category ID must be provided.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name must be provided.")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Category description cannot exceed 500 characters.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(200).WithMessage("Category image URL cannot exceed 200 characters.")
                .When(x => !string.IsNullOrEmpty(x.ImageUrl));

            RuleFor(x => x.ParentCategoryId)
                .Must(ValidatorExtension.IsValidGuid)
                .When(x => x.ParentCategoryId.HasValue)
                .WithMessage("Invalid Parent Category ID.");
        }
    }
}
