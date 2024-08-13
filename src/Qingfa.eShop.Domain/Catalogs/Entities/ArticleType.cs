using QingFa.EShop.Domain.Catalogs.ValueObjects;
using QingFa.EShop.Domain.Catalogs.ValueObjects.Identities;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class ArticleType : Entity<ArticleTypeId>
    {
        public string TypeName { get; private set; }
        public bool Active { get; private set; }
        public bool SocialSharingEnabled { get; private set; }
        public bool IsReturnable { get; private set; }
        public bool IsExchangeable { get; private set; }
        public bool PickupEnabled { get; private set; }
        public bool IsTryAndBuyEnabled { get; private set; }
        public ServiceabilityDisclaimer ServiceabilityDisclaimer { get; private set; }

        // Private constructor
        private ArticleType(ArticleTypeId id, string typeName, bool active, bool socialSharingEnabled, bool isReturnable,
            bool isExchangeable, bool pickupEnabled, bool isTryAndBuyEnabled, ServiceabilityDisclaimer serviceabilityDisclaimer)
        {
            TypeName = ValidateTypeName(typeName);
            Active = active;
            SocialSharingEnabled = socialSharingEnabled;
            IsReturnable = isReturnable;
            IsExchangeable = isExchangeable;
            PickupEnabled = pickupEnabled;
            IsTryAndBuyEnabled = isTryAndBuyEnabled;
            ServiceabilityDisclaimer = serviceabilityDisclaimer;
        }

        // Factory method
        public static ArticleType Create(ArticleTypeId id, string typeName, bool active, bool socialSharingEnabled, bool isReturnable,
            bool isExchangeable, bool pickupEnabled, bool isTryAndBuyEnabled, ServiceabilityDisclaimer serviceabilityDisclaimer)
        {
            if (id == null) throw new ArgumentException("ID must be greater than zero.", nameof(id));
            return new ArticleType(id, typeName, active, socialSharingEnabled, isReturnable, isExchangeable, pickupEnabled,
                isTryAndBuyEnabled, serviceabilityDisclaimer);
        }

        public void UpdateTypeName(string typeName)
        {
            TypeName = ValidateTypeName(typeName);
        }

        public void UpdateServiceabilityDisclaimer(ServiceabilityDisclaimer disclaimer)
        {
            ServiceabilityDisclaimer = disclaimer;
        }

        private static string ValidateTypeName(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentException("Type name cannot be null or empty.", nameof(typeName));
            return typeName;
        }

        // Protected parameterless constructor for EF Core
#pragma warning disable CS8618
        protected ArticleType()
#pragma warning restore CS8618
        {
        }
    }
}
