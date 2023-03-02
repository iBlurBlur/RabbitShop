using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Interfaces;

public interface IProductCategoryService
{
    Task<IEnumerable<SelectListItem>> GetProductCategories();
}