using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Metas
{
    public sealed class ExampleMeta : BaseEntity<Guid>, IAuditable
    {
        public string Name { get; private set; } = default!;
        public DateTimeOffset Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public string? LastModifiedBy { get; set; }

        public ExampleMeta(Guid id, string name, DateTimeOffset created, string? createdBy)
        {
            Id = id;
            Name = name;
            Created = created;
            CreatedBy = createdBy;
            LastModified = created;
            LastModifiedBy = createdBy;
        }

        public void Update(string name, string? lastModifiedBy)
        {
            Name = name;
            LastModified = DateTimeOffset.UtcNow;
            LastModifiedBy = lastModifiedBy;
        }
    }
}
