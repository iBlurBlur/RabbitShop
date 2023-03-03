namespace Application.Features.Products.Models;

public class EditProductDTO : CreateProductDTO
{
    public int ProductId { get; set; }

    public Guid Rowguid { get; set; }

    public byte[]? ThumbNailPhoto { get; set; }
}