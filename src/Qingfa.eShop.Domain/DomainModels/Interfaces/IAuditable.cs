namespace QingFa.EShop.Domain.DomainModels.Interfaces
{
    partial interface IAuditable
    {
        DateTime Created { get; }
        DateTime? Updated { get; }
    }
}
