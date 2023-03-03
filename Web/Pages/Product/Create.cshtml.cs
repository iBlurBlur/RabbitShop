using Application.Commom.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Refit;
using System.Net;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Product;

public class CreateModel : PageModel
{
    [BindProperty]
    public CreateProductViewModel CreateProductViewModel { get; set; } = new CreateProductViewModel();
    public IEnumerable<SelectListItem>? SelectListProductCategories { get; set; }

    private readonly IProductService _productService;
    private readonly IProductCategoryService _productCategoryService;

    public CreateModel(IProductService productService, IProductCategoryService productCategoryService)
    {
        _productService = productService;
        _productCategoryService = productCategoryService;
    }

    public async Task OnGetAsync() => await SetupSelectListProductCategoriesAsync();

    public async Task<IActionResult> OnPostAsync()
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

            await _productService.CreateProduct(CreateProductViewModel);
            TempData[Notification.TOAST_SUCCESS_MESSAGE] = "Create successfully";
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
}
