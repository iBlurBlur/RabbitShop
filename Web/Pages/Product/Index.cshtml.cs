using Application.Commom.Interfaces;
using Application.Features.Products.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using Refit;
using System.Net;

namespace Web.Pages.Product;

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
            await GetProducts();
            return Page();
        }
    }
}
