﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects;
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
        public OrderController(IOrderServiceAsync orderServiceAsync,IStockActionServiceAsync stockActionService)
        {
            _orderServiceAsync = orderServiceAsync;
            _stockActionServiceAsync = stockActionService;
        }
        public IActionResult CreateOrder()
        {
            return View();
        }
        /// <summary>
        /// Get a list of every order & orderDetails, and then maps the values of both of those lists into an OrderProductModel, each of those added to a list to return 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// gets the order by Id, and each OrderDetail & StockAction, deletes every stock action & orderDetail related to that order, and then deletes the order itself. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteOrder(int id)
        {
            Orders orders = new();
            List<OrderDetails> orderDetails = new();
            List<StockAction> stockActions = new();
            orders = await _orderServiceAsync.GetOrder(id);
            orderDetails = await _orderServiceAsync.GetOrderDetailsByOrder(id);
            stockActions = await _stockActionServiceAsync.GetStockActionByOrder(id);
            foreach(StockAction stockAction in stockActions)
            {
               await _stockActionServiceAsync.DeleteStockAction(stockAction.Id);
            }
            foreach(OrderDetails orderDetail in orderDetails)
            {
                await _orderServiceAsync.DeleteOrderDetails(orderDetail.OrderID,orderDetail.ProductID);
            }
            await _orderServiceAsync.DeleteOrder(id);
            return View("GetOrders");
        }
        //unfinished method
        public IActionResult ChangeOrderStatus()
        {
            return View();
        }
        /// <summary>
        /// Creates a new Order with the given values
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderProductModel model)
        {
            OrderDTO orderDTO = new OrderDTO();
            StockActionDTO stockActionDTO = new StockActionDTO();
            int orderId = 0;
            //because you would be logged in as a user, you wouldn't have a customerId saved in the session. So I designated this customerId as the admin customer id.
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
