using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class ProductViewModel: BaseProductViewModel
{
    [Required]
    public int ProductId { get; set; }

    [HiddenInput]
    public Guid Rowguid { get; set; }

    public byte[]? ThumbNailPhoto { get; set; }

    public ProductCategoryViewModel? ProductCategory { get; set; }
}
