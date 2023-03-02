using Mapster;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels;

public class CreateProductViewModel: BaseProductViewModel
{
    [DataType(DataType.Upload)]
    public IFormFile? UploadFile { get; set; }

    [Display(Name = "Category")]
    [AdaptMember("ProductCategoryId")]
    [Required]
    public int CategoryId { get; set; }
}
