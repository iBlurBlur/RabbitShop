using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Domain.Entities;
using Mapster;

namespace Application.Features.Products;

public class ProductService : IProductService
{
    private readonly IProductAPI _productAPI;

    public ProductService(IProductAPI productAPI) =>  _productAPI = productAPI;

    public async Task<IEnumerable<Product>> GetProducts() =>
        (await _productAPI.GetProducts())
            .Adapt<IEnumerable<Product>>();

    public async Task<Product?> GetProductByID(int id) =>
        (await _productAPI.GetProductByID(id))?
            .Adapt<Product?>();

    public async Task DeleteProduct(int id) => 
        await _productAPI.DeleteProduct(id);

    public async Task AddProduct(string productNumber, string name, string color, decimal price, string size, decimal? weight, string thumbnailPhotoFileName, Stream uploadFile, int productCategoryId) =>
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

    public async Task AddProduct(Product product)
    {
        var createProductDTO = product.Adapt<CreateProductDTO>();
        await _productAPI.AddProduct(createProductDTO);
    }

    public async Task EditProduct(int id, Product product)
    {
        var editProductDTO = product.Adapt<EditProductDTO>();
        await _productAPI.EditProduct(id, editProductDTO);
    }

    public async Task EditProduct(int id, int productId, string productNumber, string name, string color, decimal price, string size, decimal? weight, string thumbnailPhotoFileName, Stream uploadFile, int productCategoryId) =>
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
}
