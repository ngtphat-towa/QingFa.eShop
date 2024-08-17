namespace QingFa.EShop.Domain.Catalogs.Categories
{
    public enum CategoryStatus
    {
        Active,       // The category is active and visible.
        Inactive,     // The category is inactive and not visible.
        Archived,     // The category is archived for historical purposes.
        Pending,      // The category is pending approval.
        Disabled      // The category is disabled due to some reason.
    }
}
