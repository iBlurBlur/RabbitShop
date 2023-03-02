using Application.Commom.Constants;
using Application.Features.ProductCategories.Models;
using Refit;

namespace Application.Commom.Interfaces;

public interface IProductCategoryAPI
{
    [Get(API.PRODUCT_CATEGORY_API)]
    Task<IEnumerable<ProductCategoryDTO>> GetProductCategories();
}
