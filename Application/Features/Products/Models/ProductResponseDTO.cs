using Application.Features.ProductCategories.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Products.Models;

public class ProductResponseDTO : BaseProductDTO
{
    [Required]
    public int ProductId { get; set; }

    public Guid Rowguid { get; set; }

    public byte[]? ThumbNailPhoto { get; set; }

    public ProductCategoryDTO? ProductCategory { get; set; }
}
