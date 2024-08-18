namespace QingFa.EShop.Domain.Core.Entities
{
    public abstract class BaseEntity<IKey>
    {
        protected BaseEntity(IKey id) => Id = id;

        protected BaseEntity()
        {
            Id = default!;
        }

        public IKey Id { get; protected set; }

    }
}
