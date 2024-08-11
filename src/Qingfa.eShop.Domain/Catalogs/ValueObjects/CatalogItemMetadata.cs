using QingFa.EShop.Domain.Catalogs.Entities;
using QingFa.EShop.Domain.Catalogs.Enums;

namespace QingFa.EShop.Domain.Catalogs.ValueObjects
{
    public class CatalogItemMetadata
    {
        public Material Material { get; }
        public Season Season { get; }
        public Gender Gender { get; }
        public AgeGroup AgeGroup { get; }
        public CareInstructions CareInstructions { get; }

        public CatalogItemMetadata(Material material, Season season, Gender gender, AgeGroup ageGroup, CareInstructions careInstructions)
        {
            Material = material;
            Season = season;
            Gender = gender;
            AgeGroup = ageGroup;
            CareInstructions = careInstructions;
        }
    }
}
