using QingFa.EShop.Domain.DomainModels;

namespace QingFa.EShop.Domain.Catalogs.Entities;

public class StyleImage : Entity<int>
{
    public string SizeRepresentationUrl { get; private set; }
    public ImageData DefaultImage { get; private set; }
    public ImageData SearchImage { get; private set; }
    public ImageData BackImage { get; private set; }
    public ImageData FrontImage { get; private set; }
    public ImageData RightImage { get; private set; }

    public StyleImage(int id, string sizeRepresentationUrl, ImageData defaultImage, ImageData searchImage, ImageData backImage, ImageData frontImage, ImageData rightImage)
        : base(id)
    {
        SizeRepresentationUrl = sizeRepresentationUrl ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(sizeRepresentationUrl));
        DefaultImage = defaultImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(defaultImage));
        SearchImage = searchImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(searchImage));
        BackImage = backImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(backImage));
        FrontImage = frontImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(frontImage));
        RightImage = rightImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(rightImage));
    }

#pragma warning disable CS8618 
    protected StyleImage()
#pragma warning restore CS8618 
    {
    }

    public void UpdateSizeRepresentationUrl(string sizeRepresentationUrl)
    {
        SizeRepresentationUrl = sizeRepresentationUrl ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(sizeRepresentationUrl));
    }

    public void UpdateImages(ImageData defaultImage, ImageData searchImage, ImageData backImage, ImageData frontImage, ImageData rightImage)
    {
        DefaultImage = defaultImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(defaultImage));
        SearchImage = searchImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(searchImage));
        BackImage = backImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(backImage));
        FrontImage = frontImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(frontImage));
        RightImage = rightImage ?? throw CoreExceptionFactory.CreateNullArgumentException(nameof(rightImage));
    }
}