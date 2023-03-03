using Application.Features.ProductCategories.Models;

namespace Application.Commom.Interfaces;

public interface IProductCategoryAPI
{
    Task<IEnumerable<ProductCategoryDTO>> GetProductCategories();
}