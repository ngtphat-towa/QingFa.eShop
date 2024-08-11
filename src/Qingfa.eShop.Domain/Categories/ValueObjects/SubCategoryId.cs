namespace QingFa.EShop.Domain.Categories.ValueObjects;

public record SubCategoryId
{
    public int Value { get; set; }
    private SubCategoryId(int value)
    {
        this.Value = value;
    }
    public static SubCategoryId Create(int value)
    {
        return new(value);
    }
}
