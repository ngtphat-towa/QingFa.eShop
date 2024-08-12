using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
        /// </summary>
        protected AggregateRoot(TId id) : base(id)
        {
        }
    }
}
