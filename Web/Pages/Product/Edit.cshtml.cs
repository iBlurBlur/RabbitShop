using Application.Commom.Constants;
using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Refit;
using System.Net;
using Web.Interfaces;
using Web.Services;
using Web.ViewModels;

namespace Web.Pages.Product;

public class EditModel : PageModel
{
    [BindProperty]
    public EditProductViewModel EditProductViewModel { get; set; } = new EditProductViewModel();
    public IEnumerable<SelectListItem>? SelectListProductCategories { get; set; }

    private readonly IProductService _productService;
    private readonly IProductCategoryService _productCategoryService;

    public EditModel(IProductService productService, IProductCategoryService productCategoryService)
    {
        _productService = productService;
        _productCategoryService = productCategoryService;
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

            await _productService.EditProduct(EditProductViewModel, id);
            TempData[Notification.TOAST_SUCCESS_MESSAGE] = "Edit successfully";
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
            TempData[Notification.TOAST_ERROR_MESSAGE] = errorMessage;
            return Page();
        }
    }

    public async Task SetupSelectListProductCategoriesAsync() =>
        SelectListProductCategories = await _productCategoryService.GetProductCategories();

    private async Task<IActionResult> SetupWidgets(int id)
    {
        ProductViewModel? product = await _productService.GetProductByID(id);
        if (product == null)
        {
            return RedirectToPage("Index");
        }

        EditProductViewModel = _productService.MapEditProductViewModel(product);
        await SetupSelectListProductCategoriesAsync();
        return RedirectToPage();
    }
}
