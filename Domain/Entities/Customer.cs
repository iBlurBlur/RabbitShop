namespace Domain.Entities;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public Guid Rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<Address> Addresses { get; } = new List<Address>();
}
