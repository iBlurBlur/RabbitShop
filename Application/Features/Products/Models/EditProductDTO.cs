using Application.Features.ProductCategories.Models;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Products.Models;

public class EditProductDTO : CreateProductDTO
{
    [Required]
    public int ProductId { get; set; }

    public Guid Rowguid { get; set; }

    public byte[]? ThumbNailPhoto { get; set; }
}