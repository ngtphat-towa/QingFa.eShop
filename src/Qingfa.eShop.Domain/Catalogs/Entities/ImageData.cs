using QingFa.EShop.Domain.DomainModels;
using QingFa.EShop.Domain.Images.ValueObjects;

namespace QingFa.EShop.Domain.Catalogs.Entities
{
    public class ImageData : Entity<int>
    {
        public string ImageUrl { get; private set; }
        public string ImageType { get; private set; } // Example: "default", "search", etc.
        public Dictionary<string, Resolution> Resolutions { get; private set; } // Key: resolution, Value: Resolution

        public ImageData(int id, string imageUrl, string imageType, Dictionary<string, Resolution> resolutions)
            : base(id)
        {
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
            ImageType = imageType ?? throw new ArgumentNullException(nameof(imageType));
            Resolutions = resolutions ?? throw new ArgumentNullException(nameof(resolutions));
        }

        public void UpdateImageUrl(string imageUrl)
        {
            ImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
        }

        public void UpdateImageType(string imageType)
        {
            ImageType = imageType ?? throw new ArgumentNullException(nameof(imageType));
        }

        public void UpdateResolutions(Dictionary<string, Resolution> resolutions)
        {
            Resolutions = resolutions ?? throw new ArgumentNullException(nameof(resolutions));
        }
    }
}
