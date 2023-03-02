using Application.Commom.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;

namespace Web.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryAPI _productCategoryAPI;

    public ProductCategoryService(IProductCategoryAPI productCategoryAPI) => _productCategoryAPI = productCategoryAPI;

    public async Task<IEnumerable<SelectListItem>> GetProductCategories()
    {
        List<SelectListItem>? selectListProductCategories = (await _productCategoryAPI.GetProductCategories())
               .Select(productCategory => new SelectListItem()
               {
                   Text = productCategory.Name,
                   Value = $"{productCategory.ProductCategoryId}"
               }).ToList();

        selectListProductCategories.Insert(0, new SelectListItem()
        {
            Value = string.Empty,
            Text = "Pick one",
            Selected = true
        });

        return selectListProductCategories;
    }
}