using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Logic.DTO;


namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(OrderDTO orderDTO);
        void DeleteProductDetails(OrderDetails orderDetails);
        Orders GetOrder(int orderId);
        OrderDetails GetOrderDetails(int orderId, int productId);
        List<OrderDetails> GetOrderDetailsbyOrder(int orderId);
        List<OrderDetails> GetOrderDetailsByProduct(int productId);
        List<OrderDetails> GetOrderDetailsList();
        void UpdateOrderStatus(Orders order);
        void UpdateProductAmount(OrderDetails orderDetails);
    }
}