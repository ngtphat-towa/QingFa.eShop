namespace QingFa.EShop.Application.Features.Common.SeoInfo
{
    public record SeoMetaTransfer
    {
        public string? Title { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public string? Keywords { get; set; } = default!;
        public string? CanonicalUrl { get; set; }
        public string? Robots { get; set; }
    }
}
