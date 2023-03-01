using Application.Features.Products.Models;
using Infrastructure.HttpClient;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductAPI _productAPI;

    public IndexModel(IProductAPI productAPI) => _productAPI = productAPI;

    public IEnumerable<ProductResponseDTO> Products { get; set; } = Enumerable.Empty<ProductResponseDTO>();

    public async Task OnGetAsync() => await GetProducts();

    private async Task GetProducts() => Products = await _productAPI.GetProducts();
}
