using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class BaseProductViewModel
{
    [Required, MaxLength(100)]
    [MinLength(3, ErrorMessage = "Product name must be more than 3 characters.")]
    public string Name { get; set; } = null!;

    [Required, MaxLength(25)]
    [Display(Name = "Product Number")]
    public string ProductNumber { get; set; } = null!;

    [RegularExpression("^((?!#000080).)*$", ErrorMessage = "Please select color with value other than #000080.")]
    [MaxLength(15)]
    public string? Color { get; set; }

    [Required(ErrorMessage = "The value Price is Required")]
    [Range(0, 100_000)]
    public decimal Price { get; set; }

    [MaxLength(5)]
    public string? Size { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
    public decimal? Weight { get; set; }
}
