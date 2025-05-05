using Soup.Ordersystem.Objects;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderDTO orderDTO);
        Task DeleteProductDetails(OrderDTO orderDTO);
        Task<OrderDetails> GetOrderDetailsAsync(int orderId, int productId);
        Task<IEnumerable<OrderDetails>> GetOrderDetailsbyOrderAsync(int orderId);
        Task<IEnumerable<OrderDetails>> GetOrderDetailsByProductAsync(int productId);
        Task UpdateProductAmount(OrderDTO orderDTO);
    }
}