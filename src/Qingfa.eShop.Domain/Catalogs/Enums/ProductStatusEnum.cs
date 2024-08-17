namespace QingFa.EShop.Domain.Catalogs.Enums
{
    /// <summary>
    /// Represents the status of a product in the inventory system.
    /// </summary>
    public enum ProductStatusEnum
    {
        /// <summary>
        /// The product is available for sale.
        /// This status indicates that the product is in stock and can be purchased by customers.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The product is not currently available for sale.
        /// This status indicates that the product is out of stock or temporarily unavailable.
        /// </summary>
        Inactive = 2,

        /// <summary>
        /// The product is no longer available for sale.
        /// This status indicates that the product has been discontinued and will not be restocked.
        /// </summary>
        Discontinued = 3
    }

}
