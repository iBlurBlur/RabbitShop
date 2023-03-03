using Application.Commom.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Refit;
using System.Net;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Pages.Product;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();

    public IndexModel(IProductService productService) => _productService = productService;

    public async Task OnGetAsync() => await GetProducts();

    private async Task GetProducts() => Products = await _productService.GetProducts();

    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        try
        {
            await _productService.DeleteProduct(id);
            TempData[Notification.TOAST_SUCCESS_MESSAGE] = "Delete successfully";
            return RedirectToPage();
        }
        catch (ApiException exception)
        {
            string errorMessage = exception.Message;
            HttpStatusCode statusCode = exception.StatusCode;
            if(statusCode == HttpStatusCode.NotFound)
            {
                errorMessage = "Product Not Found";
            }
            TempData[Notification.TOAST_ERROR_MESSAGE] = errorMessage;
            await GetProducts();
            return Page();
        }
    }
}
