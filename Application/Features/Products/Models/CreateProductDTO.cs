namespace Application.Features.Products.Models;

public class CreateProductDTO : BaseProductDTO
{
    public string? FileBase64 { get; set; }

    public string? ContentType { get; set; }

    public int ProductCategoryId { get; set; }
}