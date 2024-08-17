namespace QingFa.EShop.Domain.Orders.Enums
{
    /// <summary>
    /// Represents the status of a return request.
    /// </summary>
    public enum ReturnStatusEnum
    {
        /// <summary>
        /// Return has been requested by the customer.
        /// </summary>
        Requested = 1,

        /// <summary>
        /// Return request has been approved.
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Return request has been rejected.
        /// </summary>
        Rejected = 3,

        /// <summary>
        /// Return process has been completed.
        /// </summary>
        Completed = 4
    }

}
