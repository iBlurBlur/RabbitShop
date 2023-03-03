using Application.Features.Products.Models;
using Mapster;
using Application.Commom.Interfaces;

using IProductService = Web.Interfaces.IProductService;
using IProductServiceApplication = Application.Commom.Interfaces.IProductService;

using Web.ViewModels;
using Domain.Entities;

namespace Web.Services;

public class ProductService : IProductService
{
    private readonly IProductAPI _productAPI;
    private readonly IProductServiceApplication _productServiceApplication;

    public ProductService(IProductAPI productAPI, IProductServiceApplication productServiceApplication)
    {
        _productAPI = productAPI;
        _productServiceApplication = productServiceApplication;
    }

    public async Task<IEnumerable<ProductViewModel>> GetProducts() =>
        (await _productAPI.GetProducts())
            .Adapt<IEnumerable<ProductViewModel>>();

    public async Task<ProductViewModel?> GetProductByID(int id) =>
        (await _productAPI.GetProductByID(id))?
            .Adapt<ProductViewModel>();

    public async Task CreateProduct(CreateProductViewModel createProductViewModel)
    {
        IFormFile? uploadfile = createProductViewModel.UploadFile;

        if (uploadfile == null)
        {
            var createProductDTO = createProductViewModel.Adapt<CreateProductDTO>();
            await _productAPI.AddProduct(createProductDTO);

            //var product = createProductViewModel.Adapt<Product>();
            //product.SetNameEdition(createProductViewModel.Color!);
            //await _productServiceApplication.AddProduct(product);
            return;
        }

        await _productAPI.AddProduct(
           createProductViewModel.ProductNumber,
           createProductViewModel.Name,
           createProductViewModel.Color!,
           createProductViewModel.Price,
           createProductViewModel.Size!,
           createProductViewModel.Weight,
           $"{Guid.NewGuid()}.{Path.GetExtension(uploadfile.FileName)}",
           uploadfile.OpenReadStream(),
           createProductViewModel.CategoryId
       );
    }

    public async Task EditProduct(EditProductViewModel editProductViewModel, int id)
    {
        IFormFile? uploadfile = editProductViewModel.UploadFile;

        if (uploadfile == null)
        {
            var EditProductDTO = editProductViewModel.Adapt<EditProductDTO>();
            await _productAPI.EditProduct(id, EditProductDTO);

            var product = editProductViewModel.Adapt<Product>();
            product.SetNameEdition(editProductViewModel.Color!);
            await _productServiceApplication.EditProduct(id, product);
            return;
        }
      
        await _productAPI.EditProduct(
           id,
           editProductViewModel.ProductId,
           editProductViewModel.ProductNumber,
           editProductViewModel.Name,
           editProductViewModel.Color!,
           editProductViewModel.Price,
           editProductViewModel.Size!,
           editProductViewModel.Weight,
           $"{Guid.NewGuid()}.{Path.GetExtension(uploadfile.FileName)}",
           uploadfile.OpenReadStream(),
           editProductViewModel.CategoryId
       );
    }

    public async Task DeleteProduct(int id) => await _productAPI.DeleteProduct(id);

    public EditProductViewModel MapEditProductViewModel(ProductViewModel productViewModel)
    {
        TypeAdapterConfig<ProductViewModel, EditProductViewModel>
               .NewConfig()
               .Map(dest => dest.CategoryId, src => src.ProductCategory!.ProductCategoryId);

        return TypeAdapter.Adapt<ProductViewModel, EditProductViewModel>(productViewModel);
    }
}