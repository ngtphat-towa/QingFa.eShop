using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public class ServiceabilityDisclaimer : ValueObject
    {
        public string Title { get; private set; }
        public string Desc { get; private set; }

        // Private constructor
        private ServiceabilityDisclaimer(string title, string desc)
        {
            Title = ValidateTitle(title);
            Desc = desc;
        }

        // Factory method
        public static ServiceabilityDisclaimer Create(string title, string desc)
        {
            return new ServiceabilityDisclaimer(title, desc);
        }

        public void UpdateTitle(string title)
        {
            Title = ValidateTitle(title);
        }

        private static string ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.", nameof(title));
            return title;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Title;
            yield return Desc;
        }

        // Protected parameterless constructor for EF Core
#pragma warning disable CS8618
        protected ServiceabilityDisclaimer()
#pragma warning restore CS8618
        {
        }
    }
}
