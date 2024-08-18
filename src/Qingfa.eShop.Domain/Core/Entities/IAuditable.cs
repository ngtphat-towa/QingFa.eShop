namespace QingFa.EShop.Domain.Core.Entities;

public interface IAuditable
{
    public DateTime DateCreated { get; set; }
    public string CreatedBy { get; protected set; }
    public DateTime LastModifiedDate { get; protected set; }
    public string LastModifiedBy { get; protected set; }
}
