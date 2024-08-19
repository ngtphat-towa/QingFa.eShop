using System.Linq.Expressions;
using System.Reflection;

using QingFa.EShop.Domain.Core.Exceptions;

namespace QingFa.EShop.Domain.Core.Specifications
{
    /// <summary>
    /// Base class for specifications to query entities.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        private readonly List<Expression<Func<T, object>>> _includes;
        private readonly List<string> _includeStrings;

        protected SpecificationBase()
        {
            _includes = new List<Expression<Func<T, object>>>();
            _includeStrings = new List<string>();
        }

        /// <summary>
        /// Gets or sets the criteria to filter entities.
        /// </summary>
        public Expression<Func<T, bool>>? Criteria { get; protected set; }

        /// <summary>
        /// Gets the list of expressions to include related entities.
        /// </summary>
        public IReadOnlyList<Expression<Func<T, object>>> Includes => _includes.AsReadOnly();

        /// <summary>
        /// Gets the list of related entities to include by string.
        /// </summary>
        public IReadOnlyList<string> IncludeStrings => _includeStrings.AsReadOnly();

        /// <summary>
        /// Gets or sets the expression for sorting entities in ascending order.
        /// </summary>
        public Expression<Func<T, object>>? OrderBy { get; protected set; }

        /// <summary>
        /// Gets or sets the expression for sorting entities in descending order.
        /// </summary>
        public Expression<Func<T, object>>? OrderByDescending { get; protected set; }

        /// <summary>
        /// Gets or sets the expression for grouping entities.
        /// </summary>
        public Expression<Func<T, object>>? GroupBy { get; protected set; }

        /// <summary>
        /// Gets or sets the number of entities to skip before starting to return entities.
        /// </summary>
        public int? Skip { get; set; }

        /// <summary>
        /// Gets or sets the number of entities to take from the query.
        /// </summary>
        public int? Take { get; set; }

        /// <summary>
        /// Indicates whether paging is enabled for the query.
        /// </summary>
        public bool IsPagingEnabled => Skip.HasValue || Take.HasValue;

        /// <summary>
        /// Adds an include expression for related entities.
        /// </summary>
        /// <param name="includeExpression">The include expression.</param>
        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            _includes.Add(includeExpression);
        }

        /// <summary>
        /// Adds an include string for related entities.
        /// </summary>
        /// <param name="includeString">The include string.</param>
        public void AddInclude(string includeString)
        {
            _includeStrings.Add(includeString);
        }

        /// <summary>
        /// Applies sorting in ascending order based on the property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        public void ApplyOrderBy(string propertyName)
        {
            var resolvedPropertyName = ResolvePropertyName(propertyName);
            OrderBy = CreateSortExpression(resolvedPropertyName);
        }

        /// <summary>
        /// Applies sorting in descending order based on the property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        public void ApplyOrderByDescending(string propertyName)
        {
            var resolvedPropertyName = ResolvePropertyName(propertyName);
            OrderByDescending = CreateSortExpression(resolvedPropertyName);
        }

        /// <summary>
        /// Applies paging parameters.
        /// </summary>
        /// <param name="skip">The number of entities to skip.</param>
        /// <param name="take">The number of entities to take.</param>
        public void ApplyPaging(int skip, int take)
        {
            if (skip < 0) throw new ArgumentOutOfRangeException(nameof(skip), "Skip cannot be negative.");
            if (take <= 0) throw new ArgumentOutOfRangeException(nameof(take), "Take must be greater than zero.");

            Skip = skip;
            Take = take;
        }

        /// <summary>
        /// Applies grouping based on the given expression.
        /// </summary>
        /// <param name="groupByExpression">The group by expression.</param>
        public void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }

        /// <summary>
        /// Resolves the property name to ensure it exists in the entity type.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The resolved property name if it exists, otherwise throws an exception.</returns>
        protected string ResolvePropertyName(string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (property == null)
            {
                throw CoreException.InvalidArgument(propertyName);
            }
            return property.Name;
        }

        /// <summary>
        /// Creates a sorting expression based on the property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <returns>An expression to sort by the specified property.</returns>
        protected Expression<Func<T, object>> CreateSortExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, propertyName);
            var convertedProperty = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<T, object>>(convertedProperty, parameter);
        }
    }
}
