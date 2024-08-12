using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Newtonsoft.Json;

using QingFa.EShop.Domain.Catalogs;
using QingFa.EShop.Domain.Catalogs.Enums;

namespace QingFa.EShop.Infrastructure.Persistence.Configurations
{
    public class AdditionalCostConfiguration : IEntityTypeConfiguration<AdditionalCost>
    {
        public void Configure(EntityTypeBuilder<AdditionalCost> builder)
        {
            builder.ToTable("AdditionalCosts");

            // Primary key
            builder.HasKey(ac => ac.Id);

            // Properties
            builder.Property(ac => ac.CostAdjustments)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),
                    v => JsonConvert.DeserializeObject<Dictionary<AttributeType, Dictionary<string, decimal>>>(v) ?? new Dictionary<AttributeType, Dictionary<string, decimal>>()
                )
                .HasColumnName("CostAdjustments");
        }
    }
}
