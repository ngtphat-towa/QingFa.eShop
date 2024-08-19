namespace QingFa.EShop.Domain.Core.Repositories
{
    /// <summary>
    /// Defines the contract for a unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all changes made in this unit of work to the data store.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>The number of state entries written to the data store.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
