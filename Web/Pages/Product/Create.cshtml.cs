using Application.Commom.Interfaces;
using Application.Features.ProductCategories.Models;
using Application.Features.Products.Models;
using Infrastructure.HttpClient;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Refit;
using System.Net;
using Web.ViewModels;

namespace Web.Pages.Product;

public class CreateModel : PageModel
{
    [BindProperty]
    public CreateProductViewModel CreateProductViewModel { get; set; } = new CreateProductViewModel();
    public IEnumerable<SelectListItem>? SelectListProductCategories { get; set; }

    private readonly IProductAPI _productAPI;
    private readonly IProductCategoryAPI _productCategoryAPI;

    public CreateModel(IProductAPI productAPI, IProductCategoryAPI productCategoryAPI)
    {
        _productAPI = productAPI;
        _productCategoryAPI = productCategoryAPI;
    }

    public async Task OnGetAsync()
    {
        await SetupSelectListProductCategoriesAsync();
    }

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

            IFormFile? uploadfile = CreateProductViewModel.UploadFile;

            if (uploadfile == null)
            {
                var createProductDTO = CreateProductViewModel.Adapt<CreateProductDTO>();
                await _productAPI.AddProduct(createProductDTO);
            }
            else
            {
                var streamPart = new StreamPart(uploadfile.OpenReadStream(), uploadfile.FileName, uploadfile.ContentType);

                await _productAPI.AddProduct(
                   CreateProductViewModel.ProductNumber,
                   CreateProductViewModel.Name,
                   CreateProductViewModel.Color,
                   CreateProductViewModel.Price,
                   CreateProductViewModel.Size,
                   CreateProductViewModel.Weight,
                   $"{Guid.NewGuid()}.{Path.GetExtension(uploadfile.FileName)}",
                   streamPart,
                   CreateProductViewModel.CategoryId
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
}
