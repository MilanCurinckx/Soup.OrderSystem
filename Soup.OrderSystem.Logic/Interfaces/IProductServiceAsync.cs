using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IProductServiceAsync
    {
        Task CreateProduct(ProductDTO productDTO);
        Task DeleteProduct(int id);
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetProductsList();
        Task UpdateProduct(ProductDTO productDTO);
    }
}