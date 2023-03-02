using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Refit;

namespace Infrastructure.HttpClient;

public class ProductAPI : IProductAPI
{
    private readonly IProductAPI _productAPI;

    public ProductAPI(IProductAPI productAPI) => _productAPI = productAPI;

    public async Task<IEnumerable<ProductResponseDTO>> GetProducts() => await _productAPI.GetProducts();

    public async Task DeleteProduct(int id) => await _productAPI.DeleteProduct(id);
}