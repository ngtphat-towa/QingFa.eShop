namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class CategoryProduct
    {
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = default!;

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = default!;
        public DateTime AddedDate { get; set; } 

    }
}
