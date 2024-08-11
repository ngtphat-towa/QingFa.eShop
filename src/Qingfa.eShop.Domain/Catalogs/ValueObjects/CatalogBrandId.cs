namespace QingFa.EShop.Domain.Catalogs.ValueObjects;

public record CatalogBrandId
{
    public int Value { get; set; }
    private CatalogBrandId(int value)
    {
        this.Value = value;
    }
    public static CatalogBrandId Create(int value)
    {
        return new(value);
    }
}
