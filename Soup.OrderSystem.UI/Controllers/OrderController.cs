using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.UI.Models;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private IOrderServiceAsync _orderServiceAsync;
        private IStockActionServiceAsync _stockActionServiceAsync;
        public OrderController(IOrderServiceAsync orderServiceAsync )
        {
            _orderServiceAsync = orderServiceAsync;
        }
        public IActionResult CreateOrder()
        {
            return View();
        }
        public async Task<IActionResult> GetOrders()
        {
            List<OrderProductModel> combinedOrdersList = new List<OrderProductModel>();
            List<Orders> ordersList = new List<Orders>();
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            ordersList = await _orderServiceAsync.GetOrderList();
            orderDetailsList = await _orderServiceAsync.GetOrderDetailsList();
            foreach (OrderDetails orderDetails in orderDetailsList)
            {
                OrderProductModel model = new OrderProductModel();
                model.ProductID = orderDetails.ProductID;
                model.ProductAmount = orderDetails.ProductAmount;
                model.OrderId = orderDetails.OrderID;
                foreach (Orders order in ordersList)
                {
                    model.OrderStatus = order.OrderStatus;
                    model.CustomerId = order.CustomerID;
                }
                combinedOrdersList.Add(model);
            }
            return View(combinedOrdersList);
        }
        public IActionResult DeleteOrder()
        {
            return View();
        }
        public IActionResult ChangeOrderStatus()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderProductModel model)
        {
            OrderDTO orderDTO = new OrderDTO();
            StockActionDTO stockActionDTO = new StockActionDTO();
            int orderId = 0;
            orderId = await _orderServiceAsync.CreateOrder("k1");
            orderDTO.OrderStatus = model.OrderStatus;
            orderDTO.OrderID = orderId;
            orderDTO.CustomerId = "k1";
            orderDTO.ProductID = model.ProductID;
            orderDTO.ProductAmount = model.ProductAmount;
            await _orderServiceAsync.UpdateOrderStatus(orderDTO);
            await _orderServiceAsync.CreateOrderDetails(orderDTO);
            stockActionDTO.OrderId = orderId;
            stockActionDTO.ProductId = model.ProductID;
            stockActionDTO.Amount = model.ProductAmount;
            stockActionDTO.StockActions = Objects.StockActionEnum.Reserve;
            await _stockActionServiceAsync.CreateStockAction(stockActionDTO);
            return View("GetOrders");
        }
    }
}
