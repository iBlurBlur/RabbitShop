using Application.Commom.Constants;
using Application.Features.Products.Models;
using Refit;

namespace Infrastructure.HttpClient;

public interface IProductAPI
{
    [Get(API.PRODUCT_API)]
    Task<IEnumerable<ProductResponseDTO>> GetProducts();
}
