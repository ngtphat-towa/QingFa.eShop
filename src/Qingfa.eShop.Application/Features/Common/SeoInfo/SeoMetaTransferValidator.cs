using FluentValidation;

namespace QingFa.EShop.Application.Features.Common.SeoInfo
{
    public class SeoMetaTransferValidator : AbstractValidator<SeoMetaTransfer>
    {
        public SeoMetaTransferValidator()
        {
            RuleFor(x => x.Title)
                .MaximumLength(60).WithMessage("SEO Title cannot be longer than 60 characters.")
                .When(x => !string.IsNullOrEmpty(x.Title)); 

            RuleFor(x => x.Description)
                .MaximumLength(160).WithMessage("SEO Description cannot be longer than 160 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description)); 

            RuleFor(x => x.Keywords)
                .MaximumLength(100).WithMessage("SEO Keywords cannot be longer than 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Keywords)); 

            RuleFor(x => x.CanonicalUrl)
                .MaximumLength(2000).WithMessage("Canonical URL cannot be longer than 2000 characters.")
                .When(x => !string.IsNullOrEmpty(x.CanonicalUrl)) 
                .Must(BeAValidUrl).WithMessage("Canonical URL must be a valid URL.")
                .When(x => !string.IsNullOrEmpty(x.CanonicalUrl));

            RuleFor(x => x.Robots)
                .MaximumLength(100).WithMessage("Robots meta tag cannot be longer than 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Robots)); 
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
