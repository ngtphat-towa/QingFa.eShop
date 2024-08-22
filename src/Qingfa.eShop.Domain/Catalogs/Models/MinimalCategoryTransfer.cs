namespace QingFa.EShop.Domain.Catalogs.Models
{
    public record MinimalCategoryTransfer 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid? ParentCategoryId { get; set; }
    }
}
