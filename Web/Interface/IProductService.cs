using Web.ViewModels;

namespace Web.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetProducts();
    Task<ProductViewModel?> GetProductByID(int id);
    Task CreateProduct(CreateProductViewModel createProductViewModel);
    Task EditProduct(EditProductViewModel editProductViewModel, int id);
    Task DeleteProduct(int id);
    EditProductViewModel MapEditProductViewModel(ProductViewModel productViewModel);
}