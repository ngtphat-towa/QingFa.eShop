using FluentValidation;

using QingFa.EShop.Application.Features.Common.SeoInfo;

namespace QingFa.EShop.Application.Features.BrandManagements.Update
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator(IValidator<SeoMetaTransfer> seoMetaValidator)
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Brand Id is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");

            RuleFor(x => x.SeoMeta)
                .NotNull().WithMessage("SEO Meta information is required.")
                .SetValidator(seoMetaValidator);

            RuleFor(x => x.LogoUrl)
                .MaximumLength(2000).WithMessage("Logo URL cannot be longer than 2000 characters.")
                .Must(BeAValidUrl).WithMessage("Logo URL must be a valid URL.");
        }

        private bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url)) return true; // null or empty is allowed
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
