namespace QingFa.EShop.Domain.Categories.ValueObjects;

public record CategoryId
{
    public int Value { get; set; }
    private CategoryId(int value)
    {
        this.Value = value;
    }
    public static CategoryId Create(int value)
    {
        return new(value);
    }
}
