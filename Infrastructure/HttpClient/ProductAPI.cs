using Application.Commom.Interfaces;
using Application.Features.Products.Models;

namespace Infrastructure.HttpClient;

public class ProductAPI : IProductRepository
{
    private readonly IProductAPI _productAPIInternal;

    public ProductAPI(IProductAPI productAPIInternal) => _productAPIInternal = productAPIInternal;

    public async Task<IEnumerable<ProductResponseDTO>> GetProducts() => await _productAPIInternal.GetProducts();
}