using Application.Commom.Interfaces;
using Application.Features.ProductCategories.Models;

namespace Infrastructure.HttpClient;

public class ProductCategoryAPI : IProductCategoryAPI
{
    private readonly IProductCategoryAPIClient IProductCategoryAPIClient;

    public ProductCategoryAPI(IProductCategoryAPIClient productCategoryAPIClient) => IProductCategoryAPIClient = productCategoryAPIClient;

    public async Task<IEnumerable<ProductCategoryDTO>> GetProductCategories() => await IProductCategoryAPIClient.GetProductCategories();
}