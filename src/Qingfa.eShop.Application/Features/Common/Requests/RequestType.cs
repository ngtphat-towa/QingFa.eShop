namespace QingFa.EShop.Application.Features.Common.Requests
{
    public abstract record RequestType <TId> where TId : notnull
    {
        public TId Id { get; set; } = default!;
    }
}
