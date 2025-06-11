using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    //this service is responsible for the CRUD of the Order & OrderDetails tables
    // each entry in Orderdetails is a single item from an order with the amount that was requested
    // because order only contains the Id in the database, we need to create it first, and then use that Id in orderdetails to link both of them
    public class OrderServiceAsync : IOrderServiceAsync
    {

        /// <summary>
        /// Creates a new order 
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public async Task<int> CreateOrder(string customerId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    Orders order = new();
                    OrderDetails orderDetails = new OrderDetails();
                    order.OrderStatus = OrderStatusEnum.New;
                    order.CustomerID = customerId;
                    context.Orders.Add(order);
                    await context.SaveChangesAsync();
                    return order.OrderId;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the Order" + ex.Message);
            }

        }
        /// <summary>
        /// Add a product to the order
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CreateOrderDetails(OrderDTO orderDTO)
        {
            try
            {
                using (OrderContext context = new())
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.OrderID = orderDTO.OrderID;
                    orderDetails.ProductID = orderDTO.ProductID;
                    orderDetails.ProductAmount = orderDTO.ProductAmount;
                    context.OrderDetails.Add(orderDetails);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while adding the product to the order" + ex.Message);
            }
           
        }
        /// <summary>
        /// retrieve a single ORDER, not orderDetail based on the given Id. the order contains an Id and the OrderStatus.
        /// </summary>
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Orders> GetOrder(int orderId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var Order = await context.Orders.Where(o => o.OrderId == orderId).FirstOrDefaultAsync();
                    return Order;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while searching for the order" + ex.Message);
            }
        }
        /// <summary>
        /// Retrieves a list containing every single Order. Each order contains an Id and the OrderStatus.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Orders>> GetOrderList()
        {
            try
            {
                using (OrderContext context = new())
                {
                    var OrderList = await context.Orders.ToListAsync();
                    return OrderList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something when wrong while retrieving the orders" + ex.Message);
            }
        }
        /// <summary>
        /// Returns an entire list of Orderdetails 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<OrderDetails>> GetOrderDetailsList()
        {
            try
            {
                using (OrderContext context = new())
                {
                    var orders = await context.OrderDetails.ToListAsync();
                    return orders;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the orders" + ex.Message);
            }
        }
        /// <summary>
        /// returns a List of the orderdetails of the given orderId (all of the products in the order)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<List<OrderDetails>> GetOrderDetailsByOrder(int orderId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var orderDetails = await context.OrderDetails.Where(o => o.OrderID == orderId).ToListAsync();
                    return orderDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the OrderDetails" + ex.Message);
            }
        }
        /// <summary>
        /// returns a List of the orderdetails of the given productId  (every order that has the item)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<List<OrderDetails>> GetOrderDetailsByProduct(int productId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var orderDetails = await context.OrderDetails.Where(o => o.ProductID == productId).ToListAsync();
                    return orderDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the orderdetails" + ex.Message);
            }

        }
        /// <summary>
        /// returns a specific Orderdetail of the given product & orderId (the specific product in a specific order)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<OrderDetails> GetOrderDetails(int orderId, int productId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var orderDetails = await context.OrderDetails.Where(o => o.OrderID == orderId && o.ProductID == productId).FirstOrDefaultAsync();
                    return orderDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the orderdetails" + ex.Message);
            }
        }
        /// <summary>
        /// Updates the amount of a product in a specific order
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        public async Task UpdateProductAmount(OrderDTO orderDTO)
        {
            try
            {
                var OrderToUpdate = await GetOrderDetails(orderDTO.OrderID, orderDTO.ProductID);
                using (OrderContext context = new())
                {
                    if (OrderToUpdate.ProductAmount == orderDTO.ProductAmount)
                    {
                    }
                    if (OrderToUpdate == null)
                    {
                        throw new Exception("Product Amount could not be updated because order could not be found");
                    }
                    else
                    {
                        if (OrderToUpdate.ProductAmount == orderDTO.ProductAmount)
                        { }
                        else
                        {
                            OrderToUpdate.ProductAmount = orderDTO.ProductAmount;
                            context.Update(OrderToUpdate);
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the product from the order" + ex.Message);
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
            try
            {
                var orderToUpdate = await GetOrder(orderDTO.OrderID);
                using (OrderContext context = new())
                {
                    if (orderToUpdate == null)
                    {
                        throw new Exception("Orderstatus could not be updated because order could not be found");
                    }
                    else
                    {
                        //because I can't compare an enum with an int from the DTO, I have to do it like this. Even though the value is an int inside the enum. There's probably a better way to do this, but this works.
                        switch ((int)orderDTO.OrderStatus)
                        {
                            case (int)OrderStatusEnum.New:
                                orderToUpdate.OrderStatus = OrderStatusEnum.New;
                                break;
                            case (int)OrderStatusEnum.Delivered:
                                orderToUpdate.OrderStatus = OrderStatusEnum.Delivered;
                                break;
                            case (int)OrderStatusEnum.Canceled:
                                orderToUpdate.OrderStatus = OrderStatusEnum.Canceled;
                                break;
                        }
                        context.Update(orderToUpdate);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while updating the orderstatus" + ex.Message);
            }
        }
        /// <summary>
        /// removes a product from an order
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        public async Task DeleteOrderDetails(int orderId, int productId )
        {
            try
            {
                var orderToRemove = await GetOrderDetails(orderId,productId);
                using (OrderContext context = new())
                {
                    if (orderToRemove == null)
                    {
                        throw new Exception("OrderDetails could not be found");
                    }
                    else
                    {
                        context.OrderDetails.Remove(orderToRemove);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while deleting the product from the order" + ex.Message);
            }
        }
        public async Task DeleteOrder(int orderId)
        {
            try
            {
                var orderToRemove = await GetOrder(orderId);
                using (OrderContext context = new())
                {
                    if (orderToRemove == null)
                    {
                        throw new Exception("Order could not be found");
                    }
                    else
                    {
                        context.Orders.Remove(orderToRemove);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while deleting the product from the order" + ex.Message);
            }
        }
    }
}
