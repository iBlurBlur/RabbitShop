using Application.Features.Products.Models;

namespace Application.Commom.Interfaces;

public interface IProductAPI
{
    Task<IEnumerable<ProductResponseDTO>> GetProducts();

    Task<ProductResponseDTO?> GetProductByID(int id);

    Task DeleteProduct(int id);

    Task AddProduct(CreateProductDTO createProductDTO);

    Task AddProduct(
        string productNumber,
        string name,
        string color,
        decimal price,
        string size,
        decimal? weight,
        string thumbnailPhotoFileName,
        Stream uploadFile,
        int productCategoryId
    );

    Task EditProduct( int id, EditProductDTO editProductDTO);

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
        Stream uploadFile,
        int productCategoryId
    );
}





