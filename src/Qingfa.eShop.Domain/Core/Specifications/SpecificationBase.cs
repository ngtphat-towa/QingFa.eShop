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
        public void AddInclude(Expression<Func<T, object>>? includeExpression)
        {
            if (includeExpression != null)
            {
                _includes.Add(includeExpression);
            }
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
        /// Applies sorting in ascending order based on the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <remarks>
        /// This method sets the sorting expression to sort entities in ascending order based on the given property. It uses reflection to ensure the property name is valid and creates the appropriate sorting expression.
        /// </remarks>
        public void ApplyOrderBy(string propertyName)
        {
            var resolvedPropertyName = ResolvePropertyName(propertyName);
            OrderBy = CreateSortExpression(resolvedPropertyName);
        }

        /// <summary>
        /// Applies sorting in descending order based on the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <remarks>
        /// This method sets the sorting expression to sort entities in descending order based on the given property. It uses reflection to ensure the property name is valid and creates the appropriate sorting expression.
        /// </remarks>
        public void ApplyOrderByDescending(string propertyName)
        {
            var resolvedPropertyName = ResolvePropertyName(propertyName);
            OrderByDescending = CreateSortExpression(resolvedPropertyName);
        }

        /// <summary>
        /// Applies paging parameters.
        /// </summary>
        /// <param name="pageNumber">The number of entities to page number.</param>
        /// <param name="pageSize">The number of entities to page size.</param>
        public void ApplyPaging(int pageNumber, int pageSize)
        {
            if (pageNumber < 1) throw CoreException.InvalidArgument(nameof(pageNumber), "Page number must be greater than or equal to 1.");
            if (pageSize <= 0) throw CoreException.InvalidArgument(nameof(pageSize), "Page size must be greater than zero.");

            Skip = (pageNumber - 1) * pageSize;
            Take = pageSize;
        }

        /// <summary>
        /// Applies grouping based on the specified expression.
        /// </summary>
        /// <param name="groupByExpression">The expression to group entities by.</param>
        /// <remarks>
        /// This method sets the grouping expression to group entities based on the provided expression. It is used to group results in queries.
        /// </remarks>
        public void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }

        /// <summary>
        /// Resolves the property name to ensure it exists in the entity type.
        /// </summary>
        /// <param name="propertyName">The name of the property to resolve.</param>
        /// <returns>The resolved property name if it exists, otherwise throws an exception.</returns>
        /// <exception cref="CoreException">Thrown when the property name is not found in the entity type.</exception>
        /// <remarks>
        /// This method uses reflection to check if the specified property name exists on the entity type. If the property is not found, it throws an exception.
        /// </remarks>
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
        /// Creates a sorting expression based on the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <returns>An expression to sort by the specified property.</returns>
        /// <remarks>
        /// This method uses reflection to create a lambda expression that represents sorting by the specified property. It ensures the property is converted to an object type for sorting purposes.
        /// </remarks>
        protected Expression<Func<T, object>> CreateSortExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, propertyName);
            var convertedProperty = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<T, object>>(convertedProperty, parameter);
        }

        /// <summary>
        /// Combines two expressions into a single expression using a specified binary operator.
        /// </summary>
        /// <param name="expr1">The first expression to combine.</param>
        /// <param name="expr2">The second expression to combine.</param>
        /// <param name="combineFunc">The function to combine the two expressions. This can be `Expression.AndAlso` for AND conditions or `Expression.OrElse` for OR conditions.</param>
        /// <returns>A new expression that represents the logical combination of the two provided expressions.</returns>
        /// <remarks>
        /// This method ensures that the two expressions are combined in a way that respects their parameters. It uses the specified binary operator to combine the body of the first expression with the invoked second expression.
        /// </remarks>
        protected Expression<Func<T, bool>> CombineExpressions(
            Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2,
            Func<Expression, Expression, BinaryExpression> combineFunc)
        {
            // Get the parameters of the first expression
            var parameter = expr1.Parameters;

            // Ensure the second expression has the same parameters as the first
            var invokedExpr = Expression.Invoke(expr2, parameter);

            // Combine the two expressions
            var combinedExpr = combineFunc(expr1.Body, invokedExpr);

            // Return the combined expression with the same parameters
            return Expression.Lambda<Func<T, bool>>(combinedExpr, parameter);
        }


    }
}
