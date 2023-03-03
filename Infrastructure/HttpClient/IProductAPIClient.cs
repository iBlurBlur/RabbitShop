using Application.Commom.Constants;
using Application.Features.Products.Models;
using Refit;

namespace Infrastructure.HttpClient;

public interface IProductAPIClient
{
    [Get(API.PRODUCT_API)]
    Task<IEnumerable<ProductResponseDTO>> GetProducts();

    [Get(API.PRODUCT_API + "/{id}")]
    Task<ProductResponseDTO?> GetProductByID(int id);

    [Delete(API.PRODUCT_API + "/{id}")]
    Task DeleteProduct(int id);

    [Post(API.PRODUCT_API)]
    Task AddProduct([Body(BodySerializationMethod.UrlEncoded)] CreateProductDTO createProductDTO);

    [Multipart]
    [Post(API.PRODUCT_API)]
    Task AddProduct(
       string productNumber,
       string name,
       string color,
       decimal price,
       string size,
       decimal? weight,
       string thumbnailPhotoFileName,
       StreamPart uploadFile,
       int productCategoryId
    );

    [Put(API.PRODUCT_API + "/{id}")]
    Task EditProduct(
        int id,
        [Body(BodySerializationMethod.UrlEncoded)] EditProductDTO editProductDTO
    );

    [Multipart]
    [Put(API.PRODUCT_API + "/{id}")]
    Task EditProduct(
        int id,
        int productId,
        string productNumber,
        string name,
        string color,
        decimal price,
        string size,
        decimal? weight,
        string thumbnailPhotoFileName,
        StreamPart UploadFile,
        int productCategoryId
    );
}