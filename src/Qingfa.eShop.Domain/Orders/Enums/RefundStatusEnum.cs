namespace QingFa.EShop.Domain.Orders.Enums
{
    /// <summary>
    /// Represents the status of a refund request.
    /// </summary>
    public enum RefundStatusEnum
    {
        /// <summary>
        /// Refund has been requested.
        /// </summary>
        Requested = 1,

        /// <summary>
        /// Refund request has been approved.
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Refund request has been rejected.
        /// </summary>
        Rejected = 3,

        /// <summary>
        /// Refund has been completed.
        /// </summary>
        Completed = 4
    }

}
