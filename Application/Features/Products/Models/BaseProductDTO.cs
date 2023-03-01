using Mapster;

namespace Application.Features.Products.Models;

public class BaseProductDTO
{
    public string Name { get; set; } = null!;

    public string ProductNumber { get; set; } = null!;

    public string? Color { get; set; }

    public decimal Price { get; set; }

    public string? Size { get; set; }

    public decimal? Weight { get; set; }

    [AdaptMember("ThumbnailPhotoFileName")]
    public string? FileName { get; set; }
}
