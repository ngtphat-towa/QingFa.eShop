namespace QingFa.EShop.Domain.Catalogs.Enums
{
    /// <summary>
    /// Represents different types of attributes that can be associated with a product.
    /// </summary>
    public enum AttributeOptionTypeEnum : short
    {
        /// <summary>
        /// Text attribute type.
        /// Used for attributes that store textual information.
        /// </summary>
        Text = 1,

        /// <summary>
        /// Numeric attribute type.
        /// Used for attributes that store numerical values.
        /// </summary>
        Number = 2,

        /// <summary>
        /// Date attribute type.
        /// Used for attributes that store date values.
        /// </summary>
        Date = 3,

        /// <summary>
        /// Boolean attribute type.
        /// Used for attributes that store true/false values.
        /// </summary>
        Boolean = 4,

        /// <summary>
        /// Select attribute type.
        /// Used for attributes that offer a predefined set of options for selection.
        /// </summary>
        Select = 5
    }

}
