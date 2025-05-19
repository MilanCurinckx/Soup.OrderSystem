using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects;
using Soup.Ordersystem.Objects.Order;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    //this service is responsible for the CRUD of the Order & OrderDetails tables
    // each entry in Orderdetails is a single item from an order with the amount that was requested
    // because order only contains the Id in the database, we need to create it first, and then use that Id in orderdetails to link both of them
    public class OrderService : IOrderService
    {
        private OrderContext _orderContext = new();
        /// <summary>
        /// Creates a new order and also makes an orderdetails to store the first item of the order into.
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        public async Task CreateOrderAsync(OrderDTO orderDTO)
        {
            Orders order = new();
            _orderContext.Orders.Add(order);
            await _orderContext.SaveChangesAsync();
            OrderDetails orderDetails = new();
            orderDetails.OrderID = order.OrderId;
            orderDetails.ProductID = orderDTO.ProductID;
            orderDetails.ProductAmount = orderDTO.ProductAmount;
            _orderContext.OrderDetails.Add(orderDetails);
            await _orderContext.SaveChangesAsync();
        }

        //public async Task<Orders> GetOrderAsync(int orderId)
        //{
        //    var Order = await _orderContext.Orders.Where(o => o.OrderId == orderId).FirstOrDefaultAsync();
        //    return Order;
        //}
        /// <summary>
        /// returns an Ienumerable of the orderdetails of the given orderId (all of the products in the order)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsbyOrderAsync(int orderId)
        {
            var orderDetails = await _orderContext.OrderDetails.Where(o => o.OrderID == orderId).ToListAsync();
            return orderDetails;
        }
        /// <summary>
        /// returns an Ienumerable of the orderdetails of the given productId  (every order that has the item)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsByProductAsync(int productId)
        {
            var orderDetails = await _orderContext.OrderDetails.Where(o => o.ProductID == productId).ToListAsync();
            return orderDetails;
        }
        /// <summary>
        /// returns a specific Orderdetail of the given product & orderId (the specific product in a specific order)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<OrderDetails> GetOrderDetailsAsync(int orderId, int productId)
        {
            var orderDetails = await _orderContext.OrderDetails.Where(o => o.OrderID == orderId).FirstOrDefaultAsync(o => o.ProductID == productId);
            return orderDetails;
        }
        /// <summary>
        /// Updates the amount of a product in a specific order
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        public async Task UpdateProductAmount(OrderDTO orderDTO)
        {
            var OrderToUpdate = await GetOrderDetailsAsync(orderDTO.OrderID, orderDTO.ProductID);
            if (OrderToUpdate.ProductAmount == orderDTO.ProductAmount)
            {
            }
            if (OrderToUpdate == null)
            {
                throw new Exception("ProductAmount could not be updated because order could not be found");
            }
            else 
            {
                if (OrderToUpdate.ProductAmount == orderDTO.ProductAmount)
                {}
                else
                {
                    OrderToUpdate.ProductAmount = orderDTO.ProductAmount;
                    await _orderContext.SaveChangesAsync();
                } 
            }
        }

        /// <summary>
        /// Updates the status of an order on a switch case basis, for Orderstatus is saved as an Enum
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateOrderStatus(OrderDTO orderDTO)
        {
            var orderToUpdate = await GetOrderDetailsAsync(orderDTO.OrderID, orderDTO.ProductID);
            if (orderToUpdate == null)
            {
                throw new Exception("Orderstatus could not be updated because order could not be found");
            }
            else
            {
                //because I can't compare an enum with an int from the DTO, I have to do it like this. Even though the value is an int inside the enum. There's probably a better way to do this, but this works.
                switch (orderDTO.OrderStatus)
                {
                    case (int) OrderStatusEnum.New:
                        orderToUpdate.Orders.OrderStatus = OrderStatusEnum.New; 
                        break;
                    case (int) OrderStatusEnum.Delivered:
                        orderToUpdate.Orders.OrderStatus = OrderStatusEnum.Delivered;
                        break;
                    case (int) OrderStatusEnum.Canceled:
                        orderToUpdate.Orders.OrderStatus = OrderStatusEnum.Canceled;
                        break;
                }
                await _orderContext.SaveChangesAsync();
            }
        }
        /// <summary>
        /// removes a product from an order
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        public async Task DeleteProductDetails(OrderDTO orderDTO)
        {
            var ProductToRemove = await GetOrderDetailsAsync(orderDTO.ProductID, orderDTO.ProductID);
            if (ProductToRemove == null)
            {
                throw new Exception("OrderDetails could not be found");
            }
            else 
            {
                _orderContext.OrderDetails.Remove(ProductToRemove);
                await _orderContext.SaveChangesAsync();
            }
        }
    }

}
