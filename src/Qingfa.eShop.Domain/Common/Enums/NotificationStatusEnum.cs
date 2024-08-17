namespace QingFa.EShop.Domain.Common.Enums
{
    /// <summary>
    /// Represents the status of a notification.
    /// </summary>
    public enum NotificationStatusEnum
    {
        /// <summary>
        /// Notification has been sent.
        /// </summary>
        Sent = 1,

        /// <summary>
        /// Notification has been delivered to the recipient.
        /// </summary>
        Delivered = 2,

        /// <summary>
        /// Notification failed to be delivered.
        /// </summary>
        Failed = 3
    }

}
