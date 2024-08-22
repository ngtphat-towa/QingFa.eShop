namespace QingFa.EShop.Application.Core.Models
{
    /// <summary>
    /// Represents a paginated list of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// Gets the items for the current page.
        /// </summary>
        public IReadOnlyCollection<T> Items { get; }

        /// <summary>
        /// Gets the current page number.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Gets the total number of pages.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Gets the total count of items.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        /// <param name="items">The items for the current page.</param>
        /// <param name="count">The total count of items.</param>
        /// <param name="pageNumber">The current page number.</param>
        /// <param name="pageSize">The size of each page.</param>
        public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than or equal to 1.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than or equal to 1.");

            Items = items ?? throw new ArgumentNullException(nameof(items));
            PageNumber = pageNumber;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        /// <summary>
        /// Indicates whether there is a previous page.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Indicates whether there is a next page.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;
    }
}
