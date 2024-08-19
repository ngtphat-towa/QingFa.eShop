using System.Linq.Expressions;

namespace QingFa.EShop.Domain.Core.Specifications
{
    /// <summary>
    /// Defines the criteria for querying entities.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Gets the criteria to filter entities.
        /// </summary>
        Expression<Func<T, bool>>? Criteria { get; }

        /// <summary>
        /// Gets the list of expressions to include related entities.
        /// </summary>
        IReadOnlyList<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Gets the list of related entities to include by string.
        /// </summary>
        IReadOnlyList<string> IncludeStrings { get; }

        /// <summary>
        /// Gets the expression for sorting entities in ascending order.
        /// </summary>
        Expression<Func<T, object>>? OrderBy { get; }

        /// <summary>
        /// Gets the expression for sorting entities in descending order.
        /// </summary>
        Expression<Func<T, object>>? OrderByDescending { get; }

        /// <summary>
        /// Gets the expression for grouping entities.
        /// </summary>
        Expression<Func<T, object>>? GroupBy { get; }

        /// <summary>
        /// Gets the number of entities to skip before starting to return entities.
        /// </summary>
        int? Skip { get; }

        /// <summary>
        /// Gets the number of entities to take from the query.
        /// </summary>
        int? Take { get; }

        /// <summary>
        /// Indicates whether paging is enabled for the query.
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}
