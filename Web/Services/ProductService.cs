using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Mapster;
using Refit;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services;

public class ProductService : IProductService
{
    private readonly IProductAPI _productAPI;

    public ProductService(IProductAPI productAPI) => _productAPI = productAPI;

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


