using Application.Commom.Constants;
using Application.Features.ProductCategories.Models;
using Refit;

namespace Infrastructure.HttpClient;

public interface IProductCategoryAPIClient
{
    [Get(API.PRODUCT_CATEGORY_API)]
    Task<IEnumerable<ProductCategoryDTO>> GetProductCategories();
}