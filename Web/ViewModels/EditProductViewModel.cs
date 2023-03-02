using Microsoft.AspNetCore.Mvc;

namespace Web.ViewModels;

public class EditProductViewModel : CreateProductViewModel
{
    [HiddenInput]
    public int ProductId { get; set; }

    [HiddenInput]
    public Guid Rowguid { get; set; }

    [HiddenInput]
    public byte[]? ThumbNailPhoto { get; set; }
}