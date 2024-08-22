using FluentValidation;

using QingFa.EShop.Application.Features.Common.Extensions;
using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.BrandManagements.Create
{
    internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator(IValidator<SeoMetaTransfer> seoMetaValidator)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(x => x.SeoMeta)
                .SetValidator(seoMetaValidator)
                .When(x => x.SeoMeta != null);

            RuleFor(x => x.LogoUrl)
                .MaximumLength(2000)
                .WithMessage("Logo URL cannot be longer than 2000 characters.")
                .Must(ValidatorExtension.IsValidUrl)
                .When(x => x.LogoUrl != null)
                .WithMessage("Logo URL must be a valid URL.");
        }
    }
}
