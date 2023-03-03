using Domain.Entities;

namespace Application.Commom.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();

    Task<Product?> GetProductByID(int id);

    Task DeleteProduct(int id);

    Task AddProduct(Product product);

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

    Task EditProduct(int id, Product product);

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