using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Refit;

namespace Infrastructure.HttpClient;

public class ProductAPI : IProductAPI
{
    private readonly IProductAPIClient _productAPIClient;

    public ProductAPI(IProductAPIClient productAPIClient) => _productAPIClient = productAPIClient;

    public async Task<IEnumerable<ProductResponseDTO>> GetProducts() =>
        await _productAPIClient.GetProducts();

    public async Task<ProductResponseDTO?> GetProductByID(int id) => 
        await _productAPIClient.GetProductByID(id);

    public async Task DeleteProduct(int id) =>
        await _productAPIClient.DeleteProduct(id);

    public async Task AddProduct(string productNumber, string name, string color, decimal price, string size, decimal? weight, string thumbnailPhotoFileName, Stream uploadFile, int productCategoryId) =>
        await _productAPIClient.AddProduct(
            productNumber,
            name,
            color,
            price,
            size,
            weight,
            thumbnailPhotoFileName,
            new StreamPart(uploadFile, thumbnailPhotoFileName),
            productCategoryId
        );

    public async Task AddProduct([Body(BodySerializationMethod.UrlEncoded)] CreateProductDTO createProductDTO) =>
        await _productAPIClient.AddProduct(createProductDTO);

    public async Task EditProduct(int id, [Body(BodySerializationMethod.UrlEncoded)] EditProductDTO editProductDTO) =>
          await _productAPIClient.EditProduct(id, editProductDTO);

    public async Task EditProduct(int id, int productId, string productNumber, string name, string color, decimal price, string size, decimal? weight, string thumbnailPhotoFileName, Stream uploadFile, int productCategoryId) =>
     await _productAPIClient.EditProduct(
         id,
         productId,
         productNumber,
         name,
         color,
         price,
         size,
         weight,
         thumbnailPhotoFileName,
         new StreamPart(uploadFile, thumbnailPhotoFileName),
         productCategoryId
     );
}