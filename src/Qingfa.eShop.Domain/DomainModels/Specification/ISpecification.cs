using System.Linq.Expressions;

namespace QingFa.EShop.Domain.Specifications
{
    public interface ISpecification<T>
    {
        // Criteria to filter the entities
        Expression<Func<T, bool>>? Criteria { get; }

        // Include related entities by expression
        List<Expression<Func<T, object>>> Includes { get; }

        // Include related entities by string
        List<string> IncludeStrings { get; }

        // Sort order
        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDescending { get; }

        // Grouping
        Expression<Func<T, object>>? GroupBy { get; }

        // Pagination
        int? Skip { get; }
        int? Take { get; }

        // Enable paging
        bool IsPagingEnabled { get; }
    }
}
