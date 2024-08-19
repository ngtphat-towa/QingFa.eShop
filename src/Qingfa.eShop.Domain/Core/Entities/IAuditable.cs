namespace QingFa.EShop.Domain.Core.Entities;

public interface IAuditable
{
    public DateTimeOffset Created { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public DateTimeOffset LastModified { get; protected set; }
    public string? LastModifiedBy { get; protected set; }
}
