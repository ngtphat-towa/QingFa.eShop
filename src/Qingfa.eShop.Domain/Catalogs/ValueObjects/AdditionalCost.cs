using Newtonsoft.Json;

using QingFa.EShop.Domain.Catalogs.Enums;
using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs
{
    public class AdditionalCost : Entity<int>
    {
        // Dictionary to store adjustments for different attribute types and instances
        public Dictionary<AttributeType, Dictionary<string, decimal>> CostAdjustments { get; private set; }

        public AdditionalCost()
        {
            CostAdjustments = new Dictionary<AttributeType, Dictionary<string, decimal>>();
        }

        // Method to add or update cost adjustments
        public void AddOrUpdateAdjustment(AttributeType attributeType, object attribute, decimal adjustment)
        {
            var attributeKey = attribute.ToString();
            if (!CostAdjustments.ContainsKey(attributeType))
            {
                CostAdjustments[attributeType] = new Dictionary<string, decimal>();
            }

            CostAdjustments[attributeType][attributeKey!] = adjustment;
        }

        // Method to get the adjustment for a specific attribute
        public decimal GetAdjustment(AttributeType attributeType, object attribute)
        {
            var attributeKey = attribute.ToString();
            if (CostAdjustments.TryGetValue(attributeType, out var attributes) &&
                attributes.TryGetValue(attributeKey!, out var adjustment))
            {
                return adjustment;
            }

            return 0; // Default adjustment if not found
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(CostAdjustments);
        }

        public static Dictionary<AttributeType, Dictionary<string, decimal>> Deserialize(string serializedData)
        {
            if (string.IsNullOrWhiteSpace(serializedData))
            {
                return new Dictionary<AttributeType, Dictionary<string, decimal>>();
            }

            var deserializedData = JsonConvert.DeserializeObject<Dictionary<AttributeType, Dictionary<string, decimal>>>(serializedData);
            return deserializedData ?? new Dictionary<AttributeType, Dictionary<string, decimal>>();
        }
    }
}
