using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Commons.ValueObjects;
using QingFa.EShop.Domain.Reviews.ValueObjects;
using QingFa.EShop.Domain.Users.ValueObjects;

namespace QingFa.EShop.Domain.Reviews
{
    /// <summary>
    /// Represents a review of a catalog item.
    /// </summary>
    public class Review : Entity<ReviewId>
    {
        /// <summary>
        /// Gets the unique identifier of the user who submitted the review.
        /// </summary>
        public UserId UserId { get; private set; }

        /// <summary>
        /// Gets the unique identifier of the catalog item being reviewed.
        /// </summary>
        public CatalogItemId CatalogItemId { get; private set; }

        /// <summary>
        /// Gets the rating given in the review.
        /// </summary>
        public Rating Rating { get; private set; }

        /// <summary>
        /// Gets the text of the review.
        /// </summary>
        public string Comment { get; private set; }

        /// <summary>
        /// Gets the date the review was submitted.
        /// </summary>
        public DateTime ReviewDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the review is verified as a legitimate purchase.
        /// </summary>
        public bool VerifiedPurchase { get; private set; }

        // Private constructor to prevent direct instantiation
        private Review(
            ReviewId id,
            UserId userId,
            CatalogItemId catalogItemId,
            Rating rating,
            string comment,
            DateTime reviewDate,
            bool verifiedPurchase)
            : base(id)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            CatalogItemId = catalogItemId ?? throw new ArgumentNullException(nameof(catalogItemId));
            Rating = rating ?? throw new ArgumentNullException(nameof(rating));
            Comment = !string.IsNullOrWhiteSpace(comment) ? comment : throw new ArgumentNullException(nameof(comment));
            ReviewDate = reviewDate;
            VerifiedPurchase = verifiedPurchase;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Review"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the review.</param>
        /// <param name="userId">The unique identifier of the user who submitted the review.</param>
        /// <param name="catalogItemId">The unique identifier of the catalog item being reviewed.</param>
        /// <param name="rating">The rating given in the review.</param>
        /// <param name="comment">The text of the review.</param>
        /// <param name="reviewDate">The date the review was submitted.</param>
        /// <param name="verifiedPurchase">Indicates if the review is verified.</param>
        /// <returns>A new instance of <see cref="Review"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="userId"/>, <paramref name="catalogItemId"/>, <paramref name="rating"/>, or <paramref name="comment"/> is <c>null</c> or empty.
        /// </exception>
        public static Review Create(
            ReviewId id,
            UserId userId,
            CatalogItemId catalogItemId,
            Rating rating,
            string comment,
            DateTime reviewDate,
            bool verifiedPurchase)
        {
            // Optionally perform additional validation or logic here
            return new Review(id, userId, catalogItemId, rating, comment, reviewDate, verifiedPurchase);
        }

        /// <summary>
        /// Updates the rating of the review.
        /// </summary>
        /// <param name="rating">The new rating value.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="rating"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="rating"/> is not within the expected range (typically between 1 and 5).
        /// </exception>
        public void UpdateRating(Rating rating)
        {
            if (rating is null)
            {
                throw new ArgumentNullException(nameof(rating));
            }

            // Assuming Rating has validation logic built in to ensure it is within a valid range
            Rating = rating;
        }

        /// <summary>
        /// Updates the comment of the review.
        /// </summary>
        /// <param name="comment">The new comment text.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="comment"/> is <c>null</c> or empty.
        /// </exception>
        public void UpdateComment(string comment)
        {
            Comment = !string.IsNullOrWhiteSpace(comment) ? comment : throw new ArgumentNullException(nameof(comment));
        }

        /// <summary>
        /// Marks the review as verified.
        /// </summary>
        public void VerifyPurchase()
        {
            VerifiedPurchase = true;
        }

        /// <summary>
        /// Marks the review as unverified.
        /// </summary>
        public void UnverifiedPurchase()
        {
            VerifiedPurchase = false;
        }
    }
}
