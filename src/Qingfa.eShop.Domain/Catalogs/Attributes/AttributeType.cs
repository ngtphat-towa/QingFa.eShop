namespace QingFa.EShop.Domain.Catalogs.Attributes
{
    /// <summary>
    /// Defines the type of attribute for fashion items.
    /// </summary>
    public enum AttributeType
    {
        /// <summary>
        /// A textual attribute, such as fabric type or style description.
        /// </summary>
        Text,

        /// <summary>
        /// A numeric attribute, such as size or weight.
        /// </summary>
        Number,

        /// <summary>
        /// A date attribute, useful for collection release dates or season.
        /// </summary>
        Date,

        /// <summary>
        /// A boolean attribute, for example, whether the item is on sale.
        /// </summary>
        Bool,

        /// <summary>
        /// A single-select attribute, such as color or size options.
        /// </summary>
        Select,

        /// <summary>
        /// A multi-select attribute, such as available sizes in multiple choices.
        /// </summary>
        MultiSelect
    }
}
