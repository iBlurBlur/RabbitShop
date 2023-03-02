using Application.Commom.Constants;
using Application.Features.Products.Models;
using Refit;

namespace Application.Commom.Interfaces;

public interface IProductAPI
{
    [Get(API.PRODUCT_API)]
    Task<IEnumerable<ProductResponseDTO>> GetProducts();

    [Delete(API.PRODUCT_API + "/{id}")]
    Task DeleteProduct(int id);
}
