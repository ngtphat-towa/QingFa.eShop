using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public record FashionSeason
    {
        public string Season { get; private set; } // e.g., "Spring", "Summer", "Fall", "Winter"
        public int Year { get; private set; }

        public FashionSeason(string season, int year)
        {
            if (string.IsNullOrWhiteSpace(season)) throw CoreException.NullArgument(nameof(season));
            if (year < 1900 || year > DateTime.Now.Year) throw new ArgumentOutOfRangeException(nameof(year), "Invalid fashion year.");
            Season = season;
            Year = year;
        }

        public static FashionSeason Create(string season, int year)
        {
            return new FashionSeason(season, year);
        }
    }
}
