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

    [Post(API.PRODUCT_API)]
    Task AddProduct([Body(BodySerializationMethod.UrlEncoded)] CreateProductDTO createProductDTO);

    [Multipart]
    [Post(API.PRODUCT_API)]
    Task AddProduct(
       string productNumber,
       string name,
       string? color,
       decimal price,
       string? size,
       decimal? weight,
       string? thumbnailPhotoFileName,
       StreamPart uploadFile,
       int productCategoryId
   );
}
