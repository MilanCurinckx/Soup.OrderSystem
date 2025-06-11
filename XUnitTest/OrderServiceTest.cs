using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.XunitTests
{
    public class OrderServiceTest
    {
        private IOrderServiceAsync _orderService { get; set; } = new OrderServiceAsync();
        [Fact]
        public async Task Test1()
        {
            OrderDetails details = new OrderDetails();
            IProductService productService = new ProductService();
            ICustomerService customerService = new CustomerService();
            List<OrderDetails> newOrderDetailsList = new List<OrderDetails>();
            OrderDetails newOrderDetails = new OrderDetails();
            List<Customer> customerList = new List<Customer>();
            Customer customer = new Customer();
            Product product = new Product();
            List<Product> productsList = new List<Product>();
            OrderDTO orderDTO = new OrderDTO();

            productsList = productService.GetProductsList();
            customerList = customerService.GetCustomers();
            customer = customerList.First();
            product = productsList.First();
            orderDTO.ProductID = product.ProductID;
            orderDTO.CustomerId = customer.CustomerId;
            orderDTO.ProductAmount = 1;
            int orderId =await _orderService.CreateOrder(orderDTO.CustomerId);
            orderDTO.OrderID = orderId;
            await _orderService.CreateOrderDetails(orderDTO);
            newOrderDetailsList = await _orderService.GetOrderDetailsList();
            newOrderDetails = newOrderDetailsList.Last();
            Assert.NotNull(newOrderDetails);
        }
        [Fact]
        public async Task Test2()
        {
            List<OrderDetails> detailsList = new List<OrderDetails>();
            OrderDetails details = new OrderDetails();
            detailsList = await _orderService.GetOrderDetailsList();
            details = detailsList.Last();
            Orders orders = await _orderService.GetOrder(details.OrderID);
            Assert.NotNull(orders);
        }
        [Fact]
        public async Task Test3()
        {
            List<OrderDetails> detailsList = new List<OrderDetails>();
            OrderDetails orderDetails = new OrderDetails();
            OrderDetails getOrderdetails = new OrderDetails();
            detailsList = await _orderService.GetOrderDetailsList();
            orderDetails = detailsList.Last();
            getOrderdetails = await _orderService.GetOrderDetails(orderDetails.OrderID, orderDetails.ProductID);
            Assert.NotNull(getOrderdetails);
        }
        [Fact]
        public async Task Test4()
        {
            List<OrderDetails> details = new List<OrderDetails>();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            OrderDetails orderDetails = new OrderDetails();
            details = await _orderService.GetOrderDetailsList();
            orderDetails = details.Last();
            orderDetailsList = await _orderService.GetOrderDetailsByOrder(orderDetails.OrderID);
            Assert.NotNull(orderDetailsList);
        }
        [Fact]
        public async Task Test5()
        {
            List<OrderDetails> details = new List<OrderDetails>();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            OrderDetails orderDetailsLast = new OrderDetails();
            details = await _orderService.GetOrderDetailsList();
            orderDetailsLast = details.Last();
            orderDetailsList = await _orderService.GetOrderDetailsByProduct(orderDetailsLast.ProductID);
            Assert.NotNull(orderDetailsList);
        }
        [Fact]
        public async Task Test6()
        {
            OrderDTO orderDTO = new OrderDTO();
            OrderDetails details = new OrderDetails();
            Orders orders = new Orders();
            Orders foundOrder = new Orders();
            Orders updatedOrder = new Orders();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            orderDetailsList = await _orderService.GetOrderDetailsList();
            details = orderDetailsList.Last();
            orders.OrderId = details.OrderID;
            foundOrder = await _orderService.GetOrder(details.OrderID);
            foundOrder.OrderStatus = OrderStatusEnum.Canceled;
            orderDTO.OrderID = foundOrder.OrderId;
            orderDTO.OrderStatus = foundOrder.OrderStatus;
            await _orderService.UpdateOrderStatus(orderDTO);
            updatedOrder = await _orderService.GetOrder(foundOrder.OrderId);
            Assert.Equal(foundOrder.OrderStatus, updatedOrder.OrderStatus);
        }
        [Fact]
        public async Task Test7()
        {
            OrderDTO orderDTO = new OrderDTO();
            OrderDetails details = new OrderDetails();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            OrderDetails updatedDetails = new OrderDetails();
            orderDetailsList = await _orderService.GetOrderDetailsList();
            details = orderDetailsList.Last();
            details.ProductAmount = 2;
            orderDTO.OrderID = details.OrderID;
            orderDTO.ProductID = details.ProductID;
            orderDTO.ProductAmount = details.ProductAmount;
            await _orderService.UpdateProductAmount(orderDTO);
            updatedDetails = await _orderService.GetOrderDetails(details.OrderID, details.ProductID);
            Assert.Equal(details.ProductAmount, updatedDetails.ProductAmount);
        }
        [Fact]
        public async Task Test8()
        {
            OrderDTO orderDTO = new OrderDTO();
            OrderDetails details = new OrderDetails();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            OrderDetails? deletedOrderDetails = new OrderDetails();
            orderDetailsList = await _orderService.GetOrderDetailsList();
            details = orderDetailsList.Last();
            orderDTO.OrderID = details.OrderID;
            orderDTO.ProductID = details.ProductID;
            await _orderService.DeleteOrderDetails(orderDTO.OrderID,orderDTO.ProductID);
            deletedOrderDetails = await _orderService.GetOrderDetails(details.OrderID, details.ProductID);
            Assert.Null(deletedOrderDetails);
        }
    }
}
