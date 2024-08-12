namespace QingFa.EShop.Domain.DomainModels
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot"/> class.
        /// </summary>
        protected AggregateRoot()
        {
        }
    }
}
