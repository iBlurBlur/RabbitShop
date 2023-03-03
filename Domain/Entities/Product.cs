namespace Domain.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string ProductNumber { get; set; } = null!;

    public string? Color { get; set; }

    public decimal Price { get; set; }

    public string? Size { get; set; }

    public decimal? Weight { get; set; }

    public int? ProductCategoryId { get; set; }

    public byte[]? ThumbNailPhoto { get; set; }

    public string? ThumbnailPhotoFileName { get; set; }

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public void SetNameEdition(string color)
    {
        var limitedEdition = "(Limited Edition)";
        if (color.ToUpper() == "#FFD700")
        {
            Name = $"{Name} {limitedEdition}";
        }
        else
        {
            Name = Name.Replace(limitedEdition, string.Empty);
        }
    }
}
