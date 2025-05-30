using Soup.Ordersystem.Objects;
using Soup.Ordersystem.Objects.Order;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IOrderService
    {
        void CreateOrder(OrderDetails orderDetails);
        void DeleteProductDetails(OrderDetails orderDetails);
        Orders GetOrder(int orderId);
        OrderDetails GetOrderDetails(int orderId, int productId);
        List<OrderDetails> GetOrderDetailsbyOrder(int orderId);
        List<OrderDetails> GetOrderDetailsByProduct(int productId);
        List<Orders> GetOrderList();
        void UpdateOrderStatus(Orders order);
        void UpdateProductAmount(OrderDetails orderDetails);
    }
}