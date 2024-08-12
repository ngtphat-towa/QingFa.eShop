using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels
{
    public abstract class Entity<TEntityId> : IEntity<TEntityId>, IAuditable
        where TEntityId : notnull
    {
        public Entity(TEntityId id = default!)
        {
            Id = id;
        }

        protected Entity()
        {
            Id = default!;
        }
        public TEntityId Id { get; }

        public DateTime Created { get; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

        public DateTime? Updated { get; protected set; }
    }
}
