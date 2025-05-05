using Soup.Ordersystem.Objects.Order;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductDTO productDTO);
        Task DeleteProductAsync(int id);
        Task<Products> GetProductAsync(int id);
        Task<IEnumerable<Products>> GetProductsListAsync();
        Task UpdateProductAsync(ProductDTO productDTO);
    }
}