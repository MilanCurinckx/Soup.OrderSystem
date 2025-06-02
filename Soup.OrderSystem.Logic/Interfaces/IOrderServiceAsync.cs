using Soup.Ordersystem.Objects;
using Soup.Ordersystem.Objects.Order;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public interface IOrderServiceAsync
    {
        Task CreateOrder(OrderDTO orderDTO);
        Task DeleteOrderDetails(OrderDTO orderDTO);
        Task<Orders> GetOrder(int orderId);
        Task<OrderDetails> GetOrderDetails(int orderId, int productId);
        Task<List<OrderDetails>> GetOrderDetailsbyOrder(int orderId);
        Task<List<OrderDetails>> GetOrderDetailsByProduct(int productId);
        Task<List<OrderDetails>> GetOrderDetailsList();
        Task UpdateOrderStatus(OrderDTO orderDTO);
        Task UpdateProductAmount(OrderDTO orderDTO);
    }
}