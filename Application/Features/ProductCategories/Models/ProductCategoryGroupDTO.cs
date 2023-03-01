using Mapster;

namespace Application.Features.ProductCategories.Models;

public class ProductCategoryGroupDTO
{
    [AdaptMember("InverseParentProductCategory")]
    public IEnumerable<ProductCategoryDTO> Categories { get; set; } = null!;

    public string Name { get; set; } = null!;
}
