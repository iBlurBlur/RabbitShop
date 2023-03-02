using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Refit;

namespace Infrastructure.HttpClient;

public class ProductAPI : IProductAPI
{
    private readonly IProductAPI _productAPI;

    public ProductAPI(IProductAPI productAPI) => _productAPI = productAPI;

    public async Task<IEnumerable<ProductResponseDTO>> GetProducts() =>
        await _productAPI.GetProducts();

    public async Task<ProductResponseDTO> GetProductByID(int id) => 
        await _productAPI.GetProductByID(id);

    public async Task DeleteProduct(int id) =>
        await _productAPI.DeleteProduct(id);

    public async Task AddProduct(string productNumber, string name, string? color, decimal price, string? size, decimal? weight, string? thumbnailPhotoFileName, StreamPart uploadFile, int productCategoryId) =>
        await _productAPI.AddProduct(
            productNumber,
            name,
            color,
            price,
            size,
            weight,
            thumbnailPhotoFileName,
            uploadFile,
            productCategoryId
        );

    public async Task AddProduct([Body(BodySerializationMethod.UrlEncoded)] CreateProductDTO createProductDTO) =>
        await _productAPI.AddProduct(createProductDTO);

    public async Task EditProduct(int id, int productId, string productNumber, string name, string? color, decimal price, string? size, decimal? weight, string? thumbnailPhotoFileName, StreamPart uploadFile, int productCategoryId) => 
        await _productAPI.EditProduct(
            id,
            productId,
            productNumber,
            name,
            color,
            price,
            size,
            weight,
            thumbnailPhotoFileName,
            uploadFile,
            productCategoryId
        );

    public async Task EditProduct(int id, [Body(BodySerializationMethod.UrlEncoded)] EditProductDTO editProductDTO) =>
          await _productAPI.EditProduct(id, editProductDTO);
}