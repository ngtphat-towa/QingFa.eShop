namespace QingFa.EShop.Application.Features.Common.Responses
{
    public record BasicResponse<TId>
    {
        public TId Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}
