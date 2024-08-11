using QingFa.EShop.Domain.DomainModels.Bases;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects;

public class Dimensions : ValueObject
{
    public decimal Length { get; }
    public decimal Width { get; }
    public decimal Height { get; }

    public Dimensions(decimal length, decimal width, decimal height)
    {
        Length = length;
        Width = width;
        Height = height;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Length;
        yield return Width;
        yield return Height;
    }
}
