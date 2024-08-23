using QingFa.EShop.Domain.Core.Enums;

namespace QingFa.EShop.Application.Features.CategoryManagements.UpdateCategory
{
    public record UpdateCategoryRequest
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public string? ImageUrl { get; init; }
        public Guid? ParentCategoryId { get; init; }
        public EntityStatus Status { get; init; }
    }
}
