using Application.Features.Products.Models;

namespace Application.Commom.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<ProductResponseDTO>> GetProducts();
}
