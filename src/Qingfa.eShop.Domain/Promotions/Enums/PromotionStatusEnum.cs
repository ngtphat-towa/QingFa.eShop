namespace QingFa.EShop.Domain.Promotions.Enums
{
    /// <summary>
    /// Represents the status of a promotion.
    /// </summary>
    public enum PromotionStatusEnum
    {
        /// <summary>
        /// Promotion is currently active.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Promotion is not currently active.
        /// </summary>
        Inactive = 2,

        /// <summary>
        /// Promotion has expired.
        /// </summary>
        Expired = 3
    }

}
