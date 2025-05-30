using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects;
using Soup.Ordersystem.Objects.Order;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    //this service is responsible for the CRUD of the Order & OrderDetails tables
    // each entry in Orderdetails is a single item from an order with the amount that was requested
    // because order only contains the Id in the database, we need to create it first, and then use that Id in orderdetails to link both of them
    public class OrderService : IOrderService
    {

        /// <summary>
        /// Creates a new order and also makes an orderdetails to store the first item of the order into.
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public void CreateOrder(OrderDetails orderDetails)
        {
            try
            {
                using (OrderContext context = new())
                {
                    Orders order = new();
                    context.Orders.Add(order);
                    context.SaveChanges();
                    OrderDetails newOrderDetails = new();
                    orderDetails.OrderID = order.OrderId;
                    orderDetails.ProductID = orderDetails.ProductID;
                    orderDetails.ProductAmount = orderDetails.ProductAmount;
                    context.OrderDetails.Add(orderDetails);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the Order");
            }

        }

        public Orders GetOrder(int orderId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var Order = context.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
                    return Order;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while searching for the order" + ex.Message);
            }
        }
        public List<Orders> GetOrderList()
        {
            try
            {
                using (OrderContext context = new())
                {
                    var orders = context.Orders.ToList();
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
        public List<OrderDetails> GetOrderDetailsbyOrder(int orderId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var orderDetails = context.OrderDetails.Where(o => o.OrderID == orderId).ToList();
                    return orderDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the OrderDetails");
            }
        }
        /// <summary>
        /// returns a List of the orderdetails of the given productId  (every order that has the item)
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<OrderDetails> GetOrderDetailsByProduct(int productId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var orderDetails = context.OrderDetails.Where(o => o.ProductID == productId).ToList();
                    return orderDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the orderdetails");
            }

        }
        /// <summary>
        /// returns a specific Orderdetail of the given product & orderId (the specific product in a specific order)
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public OrderDetails GetOrderDetails(int orderId, int productId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var orderDetails = context.OrderDetails.Where(o => o.OrderID == orderId).FirstOrDefault(o => o.ProductID == productId);
                    return orderDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the orderdetails");
            }
        }
        /// <summary>
        /// Updates the amount of a product in a specific order
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public void UpdateProductAmount(OrderDetails orderDetails)
        {
            try
            {
                var OrderToUpdate = GetOrderDetails(orderDetails.OrderID, orderDetails.ProductID);
                using (OrderContext context = new())
                {
                    if (OrderToUpdate.ProductAmount == orderDetails.ProductAmount)
                    {
                    }
                    if (OrderToUpdate == null)
                    {
                        throw new Exception("ProductAmount could not be updated because order could not be found");
                    }
                    else
                    {
                        if (OrderToUpdate.ProductAmount == orderDetails.ProductAmount)
                        { }
                        else
                        {
                            OrderToUpdate.ProductAmount = orderDetails.ProductAmount;
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the product from the order");
            }
        }

        /// <summary>
        /// Updates the status of an order on a switch case basis, for Orderstatus is saved as an Enum
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void UpdateOrderStatus(Orders order)
        {
            try
            {
                var orderToUpdate = GetOrder(order.OrderId);
                using (OrderContext context = new())
                {
                    if (orderToUpdate == null)
                    {
                        throw new Exception("Orderstatus could not be updated because order could not be found");
                    }
                    else
                    {
                        //because I can't compare an enum with an int from the DTO, I have to do it like this. Even though the value is an int inside the enum. There's probably a better way to do this, but this works.
                        switch (order.OrderStatus)
                        {
                            case OrderStatusEnum.New:
                                orderToUpdate.OrderStatus = OrderStatusEnum.New;
                                break;
                            case OrderStatusEnum.Delivered:
                                orderToUpdate.OrderStatus = OrderStatusEnum.Delivered;
                                break;
                            case OrderStatusEnum.Canceled:
                                orderToUpdate.OrderStatus = OrderStatusEnum.Canceled;
                                break;
                        }
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while updating the orderstatus");
            }
        }
        /// <summary>
        /// removes a product from an order
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public void DeleteProductDetails(OrderDetails orderDetails)
        {
            try
            {
                var ProductToRemove = GetOrderDetails(orderDetails.ProductID, orderDetails.ProductID);
                using (OrderContext context = new())
                {
                    if (ProductToRemove == null)
                    {
                        throw new Exception("OrderDetails could not be found");
                    }
                    else
                    {
                        context.OrderDetails.Remove(ProductToRemove);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while deleting the product from the order");
            }
        }
    }
}
