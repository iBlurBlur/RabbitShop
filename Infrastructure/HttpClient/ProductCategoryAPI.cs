using Application.Commom.Interfaces;
using Application.Features.ProductCategories.Models;

namespace Infrastructure.HttpClient;

public class ProductCategoryAPI : IProductCategoryAPI
{
    private readonly IProductCategoryAPI _productCategoryAPI;

    public ProductCategoryAPI(IProductCategoryAPI productCategoryAPI) => _productCategoryAPI = productCategoryAPI;

    public async Task<IEnumerable<ProductCategoryDTO>> GetProductCategories() => await _productCategoryAPI.GetProductCategories();
}