using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Infrastructure.HttpClient;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Refit;
using System.Net;
using Web.ViewModels;

namespace Web.Pages.Product;

public class EditModel : PageModel
{
    [BindProperty]
    public EditProductViewModel EditProductViewModel { get; set; } = new EditProductViewModel();
    public IEnumerable<SelectListItem>? SelectListProductCategories { get; set; }

    private readonly IProductAPI _productAPI;
    private readonly IProductCategoryAPI _productCategoryAPI;

    public EditModel(IProductAPI productAPI, IProductCategoryAPI productCategoryAPI)
    {
        _productAPI = productAPI;
        _productCategoryAPI = productCategoryAPI;
    }

    public async Task OnGetAsync(int id) => await SetupWidgets(id);

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                string errorMessages = string.Join("; ", ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage));

                throw new Exception(errorMessages);
            }

            if (EditProductViewModel.ProductId != id)
            {
                throw new Exception("Product invalid");
            }

            IFormFile? uploadfile = EditProductViewModel.UploadFile;

            if (uploadfile == null)
            {
                var EditProductDTO = EditProductViewModel.Adapt<EditProductDTO>();
                await _productAPI.EditProduct(id, EditProductDTO);
            }
            else
            {
                var streamPart = new StreamPart(uploadfile.OpenReadStream(), uploadfile.FileName, uploadfile.ContentType);

                await _productAPI.EditProduct(
                   id,
                   EditProductViewModel.ProductId,
                   EditProductViewModel.ProductNumber,
                   EditProductViewModel.Name,
                   EditProductViewModel.Color,
                   EditProductViewModel.Price,
                   EditProductViewModel.Size,
                   EditProductViewModel.Weight,
                   $"{Guid.NewGuid()}.{Path.GetExtension(uploadfile.FileName)}",
                   streamPart,
                   EditProductViewModel.CategoryId
               );
            }

            return RedirectToPage("Index");
        }
        catch (ApiException exception)
        {
            string errorMessage = exception.Message;
            HttpStatusCode statusCode = exception.StatusCode;
            if (statusCode == HttpStatusCode.BadRequest)
            {
                errorMessage = "Product Invalid";
            }
            await SetupSelectListProductCategoriesAsync();
            return Page();
        }
    }

    public async Task SetupSelectListProductCategoriesAsync()
    {
        List<SelectListItem>? selectListProductCategories = (await _productCategoryAPI.GetProductCategories()).Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = $"{c.ProductCategoryId}"
        }).ToList();

        selectListProductCategories.Insert(0, new SelectListItem()
        {
            Value = string.Empty,
            Text = "Pick one",
            Selected = true
        });

        SelectListProductCategories = selectListProductCategories;
    }

    private async Task<IActionResult> SetupWidgets(int id)
    {
        var product = (await _productAPI.GetProductByID(id)).Adapt<ProductViewModel?>();
        if (product == null)
        {
            return RedirectToPage("Index");
        }

        EditProductViewModel = MapEditProductViewModel(product);
        await SetupSelectListProductCategoriesAsync();
        return RedirectToPage();
    }

    public EditProductViewModel MapEditProductViewModel(ProductViewModel productViewModel)
    {
        TypeAdapterConfig<ProductViewModel, EditProductViewModel>
               .NewConfig()
               .Map(dest => dest.CategoryId, src => src.ProductCategory!.ProductCategoryId);
        return TypeAdapter.Adapt<ProductViewModel, EditProductViewModel>(productViewModel);
    }
}
