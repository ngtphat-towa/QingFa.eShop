namespace QingFa.EShop.Domain.Orders.Enums
{
    /// <summary>
    /// Represents the status of an order.
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// Order has been placed but not yet processed.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Order is being processed.
        /// </summary>
        Processing = 2,

        /// <summary>
        /// Order has been shipped to the customer.
        /// </summary>
        Shipped = 3,

        /// <summary>
        /// Order has been delivered to the customer.
        /// </summary>
        Delivered = 4,

        /// <summary>
        /// Order has been cancelled.
        /// </summary>
        Cancelled = 5
    }

}
