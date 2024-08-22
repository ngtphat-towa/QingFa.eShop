using FluentValidation;

using QingFa.EShop.Application.Features.Common.Extensions;
using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.CategoryManagements.CreateCategory
{
    internal class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator(IValidator<SeoMetaTransfer> seoMetaValidator)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

            RuleFor(x => x.ImageUrl)
                .Must(ValidatorExtension.IsValidUrl).WithMessage("ImageUrl must be a valid URL.")
                .When(x => !string.IsNullOrEmpty(x.ImageUrl));

            RuleFor(x => x.ParentCategoryId)
                .Must(ValidatorExtension.IsValidGuid).WithMessage("ParentCategoryId must be a valid GUID.")
                .When(x => x.ParentCategoryId.HasValue);

            RuleFor(x => x.Status)
                .IsInEnum().When(x => x.Status.HasValue).WithMessage("Invalid status value.");

            RuleFor(x => x.SeoMeta)
                .SetValidator(seoMetaValidator)
                .When(x => x.SeoMeta != null);
        }
    }
}
