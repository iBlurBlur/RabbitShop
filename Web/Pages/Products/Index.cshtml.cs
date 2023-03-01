using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Products;

public class IndexModel : PageModel
{
    public List<dynamic> Products { get; set; } = new List<dynamic>();  

    public void OnGet()
    {
        for (int i = 0; i < 5; i++)
        {
            Products.Add(new {
              ProductId = i + 1,
              ProductNumber = Guid.NewGuid(),
              Name = $"Item_{i + 1}",
              ProductCategory = new {
                    Name = "Mobile"
              },
              Price = new Random().Next(),
            });
        }
    }
}
