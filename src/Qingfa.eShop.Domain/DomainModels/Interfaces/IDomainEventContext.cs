namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Represents a context for managing domain events within a given scope.
    /// This interface provides a method to retrieve the domain events associated
    /// with the current context.
    /// </summary>
    public interface IDomainEventContext
    {
        /// <summary>
        /// Retrieves a collection of domain events that are part of the current context.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{IDomainEvent}"/> containing the domain events
        /// associated with the context.
        /// </returns>
        IEnumerable<IDomainEvent> GetDomainEvents();
    }
}
