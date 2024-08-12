namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public record FashionYear
    {
        public int Year { get; private set; }

        public FashionYear(int year)
        {
            if (year < 1900 || year > DateTime.Now.Year) throw new ArgumentOutOfRangeException(nameof(year), "Invalid fashion year.");
            Year = year;
        }

        public static FashionYear Create(int year)
        {
            return new FashionYear(year);
        }
    }
}
