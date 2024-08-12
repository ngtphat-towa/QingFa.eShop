using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs
{
    public sealed class UnitOfMeasure : ValueObject
    {
        public string Name { get; }
        public string Symbol { get; }

        private UnitOfMeasure(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
        }

        public static UnitOfMeasure Create(string name="", string symbol ="")
        {
            return new UnitOfMeasure(name, symbol);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Symbol;
        }

#pragma warning disable CS8618
        private UnitOfMeasure()
#pragma warning restore CS8618
        {

        }
    }
}
