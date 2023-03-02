using Application.Commom.Constants;
using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using Refit;
using System.Net;

namespace Web.Pages.Product;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class IndexModel : PageModel
{
    private readonly IProductAPI _productAPI;

    public IndexModel(IProductAPI productAPI) => _productAPI = productAPI;

    public IEnumerable<ProductResponseDTO> Products { get; set; } = Enumerable.Empty<ProductResponseDTO>();

    public async Task OnGetAsync() => await GetProducts();

    private async Task GetProducts() => Products = await _productAPI.GetProducts();

    public async Task<IActionResult> OnGetDeleteAsync(int id)
    {
        try
        {
            await _productAPI.DeleteProduct(id);
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
