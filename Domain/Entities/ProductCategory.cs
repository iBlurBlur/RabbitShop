namespace Domain.Entities;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public int? ParentProductCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ProductCategory> InverseParentProductCategory { get; } = new List<ProductCategory>();

    public virtual ProductCategory? ParentProductCategory { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
