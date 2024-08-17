namespace QingFa.EShop.Domain.Orders.Enums
{
    /// <summary>
    /// Represents the status of a transaction.
    /// </summary>
    public enum TransactionStatusEnum
    {
        /// <summary>
        /// Transaction is pending and not yet completed.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Transaction has been completed successfully.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// Transaction has failed.
        /// </summary>
        Failed = 3
    }

}
