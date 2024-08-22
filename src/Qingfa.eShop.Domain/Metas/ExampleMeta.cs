using QingFa.EShop.Domain.Core.Entities;

namespace QingFa.EShop.Domain.Metas
{
    public sealed class ExampleMeta : AuditEntity
    {
        public string Name { get; private set; } = default!;

        public ExampleMeta(Guid id, string name, DateTimeOffset created, string? createdBy): base(id)
        {
            Id = id;
            Name = name;
            Created = created;
            CreatedBy = createdBy;
            LastModified = created;
            LastModifiedBy = createdBy;
        }

        private ExampleMeta(): base(default!)
        {
        }

        public void Update(string name, string? lastModifiedBy)
        {
            Name = name;
            LastModified = DateTimeOffset.UtcNow;
            LastModifiedBy = lastModifiedBy;
        }
    }
}
