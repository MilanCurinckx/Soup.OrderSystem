using Soup.Ordersystem.Objects;
using Soup.Ordersystem.Objects.Customer;
using Soup.Ordersystem.Objects.Order;
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
        private IOrderService _orderService {  get; set; } = new OrderService();
        [Fact]
        public void Test1()
        {
            OrderDetails details = new OrderDetails();
            IProductService productService = new ProductService();
            ICustomerService customerService = new CustomerService();
            OrderDetails? newOrderDetails = new OrderDetails();
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
            _orderService.CreateOrder(orderDTO);

            newOrderDetails = _orderService.GetOrderDetailsList().Last();
            Assert.NotNull(newOrderDetails);
        }
        [Fact]
        public void Test2()
        {
            OrderDetails details = new OrderDetails();
            details = _orderService.GetOrderDetailsList().Last();
            Orders orders = _orderService.GetOrder(details.OrderID);
            Assert.NotNull(orders);
        }
        [Fact]
        public void Test3()
        {
            OrderDetails details = new OrderDetails();
            OrderDetails orderDetails = new OrderDetails();
            details = _orderService.GetOrderDetailsList().Last();
            orderDetails = _orderService.GetOrderDetails(details.OrderID, details.ProductID);
            Assert.NotNull(orderDetails);
        }
        [Fact]
        public void Test4()
        {
            OrderDetails details = new OrderDetails();
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            details =_orderService.GetOrderDetailsList().Last();
            orderDetails = _orderService.GetOrderDetailsbyOrder(details.OrderID);
            Assert.NotNull(orderDetails);
        }
        [Fact]
        public void Test5()
        {
            OrderDetails details = new OrderDetails();
            List<OrderDetails> orderDetails = new List<OrderDetails>();
            details = _orderService.GetOrderDetailsList().Last();
            orderDetails = _orderService.GetOrderDetailsByProduct(details.ProductID);
            Assert.NotNull(orderDetails);
        }
        [Fact]
        public void Test6()
        {
            OrderDetails details = new OrderDetails();
            Orders orders = new Orders();
            Orders foundOrder = new Orders();
            Orders updatedOrder = new Orders();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            orderDetailsList = _orderService.GetOrderDetailsList();
            details= orderDetailsList.Last();
            orders.OrderId = details.OrderID;
            foundOrder = _orderService.GetOrder(details.OrderID);
            foundOrder.OrderStatus = OrderStatusEnum.Canceled;
            _orderService.UpdateOrderStatus(foundOrder);
            updatedOrder = _orderService.GetOrder(foundOrder.OrderId);
            Assert.Equal(foundOrder.OrderStatus, updatedOrder.OrderStatus);
        }
        [Fact]
        public void Test7()
        {
            OrderDetails details= new OrderDetails();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            OrderDetails updatedDetails = new OrderDetails();
            orderDetailsList= _orderService.GetOrderDetailsList();
            details = orderDetailsList.Last();
            details.ProductAmount = 2;
            _orderService.UpdateProductAmount(details);
            updatedDetails = _orderService.GetOrderDetails(details.OrderID,details.ProductID);
            Assert.Equal(details.ProductAmount, updatedDetails.ProductAmount);
        }
        [Fact]
        public void Test8()
        {
            OrderDetails details= new OrderDetails();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            OrderDetails? deletedOrderDetails = new OrderDetails();
            orderDetailsList = _orderService.GetOrderDetailsList();
            details = orderDetailsList.Last();
            _orderService.DeleteProductDetails(details);
            deletedOrderDetails = _orderService.GetOrderDetails(details.OrderID,details.ProductID);
            Assert.Null(deletedOrderDetails);
        }
    }
}
