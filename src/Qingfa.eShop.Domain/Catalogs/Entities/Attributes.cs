namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class Attributes
    {
        public string? Material { get; }
        public string? Season { get; }
        public string? Gender { get; }
        public string? AgeGroup { get; }
        public string? CareInstructions { get; }

        public Attributes(string? material, string? season, string? gender, string? ageGroup, string? careInstructions)
        {
            Material = material;
            Season = season;
            Gender = gender;
            AgeGroup = ageGroup;
            CareInstructions = careInstructions;
        }
    }
}
