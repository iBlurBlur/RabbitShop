namespace Domain.Entities;

public partial class Address
{
    public int AddressId { get; set; }

    public string Address1 { get; set; } = null!;

    public string City { get; set; } = null!;

    public string StateProvince { get; set; } = null!;

    public string CountryRegion { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; } = new List<Customer>();
}
