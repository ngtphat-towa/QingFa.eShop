namespace QingFa.EShop.Domain.Orders.Enums
{
    /// <summary>
    /// Represents the status of a purchase order.
    /// </summary>
    public enum PurchaseOrderStatus
    {
        /// <summary>
        /// Purchase order is awaiting action.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Purchase order has been approved.
        /// </summary>
        Approved = 2,

        /// <summary>
        /// Purchase order has been rejected.
        /// </summary>
        Rejected = 3,

        /// <summary>
        /// Purchase order has been completed.
        /// </summary>
        Completed = 4
    }

}
