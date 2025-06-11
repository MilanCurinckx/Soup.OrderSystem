using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.UI.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IOrderServiceAsync _orderService;
        private readonly IProductServiceAsync _productService;
        private readonly IStockActionServiceAsync _stockActionService;
        public ShoppingCartController(IOrderServiceAsync service, IProductServiceAsync productService, IStockActionServiceAsync stockActionService)
        {
            _orderService = service;
            _productService = productService;
            _stockActionService = stockActionService;
        }

        public async Task<IActionResult> Add(int id)
        {
            StockActionDTO actionDTO = new StockActionDTO();
            OrderDTO orderDTO = new OrderDTO();
            OrderDetails orderDetails = new OrderDetails();
            List<OrderDetails> productsInOrder = new List<OrderDetails>();
            int amountToAdd = 1;
            string customerId = "";
            int? orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                if (User.Identity?.IsAuthenticated == true && User.IsInRole("Customer"))
                {
                    customerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    HttpContext.Session.SetInt32("OrderId", await _orderService.CreateOrder(customerId));
                    orderId = HttpContext.Session.GetInt32("OrderId");
                }
                else
                {
                    return RedirectToAction("CustomerLogin", "Login");
                }
            }
            int notNullOrderId = orderId.Value;

            orderDetails = await _orderService.GetOrderDetails(notNullOrderId, id);
            if (orderDetails == null)
            {
                orderDTO.OrderID = notNullOrderId;
                orderDTO.ProductID = id;
                orderDTO.ProductAmount = amountToAdd;
                await _orderService.CreateOrderDetails(orderDTO);
            }
            else
            {
                orderDetails.ProductAmount = orderDetails.ProductAmount++;
            }
            actionDTO.ProductId = id;
            actionDTO.OrderId = notNullOrderId;
            actionDTO.Amount = amountToAdd;
            actionDTO.StockActions = Objects.StockActionEnum.Reserve;
            await _stockActionService.CreateStockAction(actionDTO);
            return RedirectToAction("ShoppingCart");
        }
        public async Task<IActionResult> ShoppingCart()
        {
            List<OrderProductModel> productModels = new List<OrderProductModel>();
            int? orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                return RedirectToAction("CustomerLogin", "Login");
            }
            else
            {
                int notNullOrder = orderId.Value;
                List<OrderDetails> orderDetailList = await _orderService.GetOrderDetailsByOrder(notNullOrder);

                if (orderDetailList != null)
                {
                    foreach (OrderDetails orderDetail in orderDetailList)
                    {
                        OrderProductModel productModel = new OrderProductModel();
                        Product product = await _productService.GetProduct(orderDetail.ProductID);
                        productModel.ProductID = orderDetail.ProductID;
                        productModel.ProductName = product.ProductName;
                        productModel.ProductAmount = orderDetail.ProductAmount;
                        productModels.Add(productModel);
                    }
                }
            }
            return View(productModels);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAmount(OrderProductModel model)
        {
            OrderDTO orderDTO = new();
            StockActionDTO stockActionDTO = new StockActionDTO();
            OrderDetails orderDetails = new();
            string customerId = "";
            int stockCheck = 0;
            stockCheck = await _stockActionService.GetAvailableStockAmount(model.ProductID);
            if (stockCheck < stockActionDTO.Amount)
            {
                ViewData["Error"] = "Not enough items in stock for this operation, please add fewer items";
                return View("Details", model);
            }
            int? orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                if (User.Identity?.IsAuthenticated == true && User.IsInRole("Customer"))
                {
                    customerId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                    HttpContext.Session.SetInt32("OrderId", await _orderService.CreateOrder(customerId));
                    orderId = HttpContext.Session.GetInt32("OrderId");
                }
                else
                {
                    return RedirectToAction("CustomerLogin", "Login");
                }
            }
            int notNullOrder = orderId.Value;
            orderDetails = await _orderService.GetOrderDetails(orderId: notNullOrder, productId: model.ProductID);
            if (orderDetails == null)
            {
                orderDTO.OrderID = notNullOrder;
                orderDTO.ProductID = model.ProductID;
                orderDTO.ProductAmount = model.ProductAmount;
                await _orderService.CreateOrderDetails(orderDTO);
                orderDetails = await _orderService.GetOrderDetails(orderId: notNullOrder, productId: model.ProductID);
            }
            orderDTO.ProductAmount = model.ProductAmount;
            orderDTO.ProductID = model.ProductID;
            orderDTO.OrderID = notNullOrder;
            await _orderService.UpdateProductAmount(orderDTO);
            stockActionDTO.OrderId = notNullOrder;
            stockActionDTO.ProductId = model.ProductID;
            if (model.ProductID > orderDetails.ProductAmount)
            {
                stockActionDTO.Amount = model.ProductAmount - orderDetails.ProductAmount;
                stockActionDTO.StockActions = Objects.StockActionEnum.Reserve;
            }
            else
            {
                stockActionDTO.Amount = orderDetails.ProductAmount - model.ProductAmount;
                stockActionDTO.StockActions = Objects.StockActionEnum.Add;
            }
            await _stockActionService.CreateStockAction(stockActionDTO);
            return RedirectToAction("ShoppingCart");
        }
        public async Task<IActionResult> Remove(int id)
        {
            int? orderId = HttpContext.Session.GetInt32("OrderId");
            StockActionDTO stockActionDTO = new();
            OrderDetails orderDetails = new();
            if (orderId != null)
            {
                int notNullOrder = orderId.Value;
                orderDetails = await _orderService.GetOrderDetails(notNullOrder, id);
                stockActionDTO.Amount = orderDetails.ProductAmount;
                stockActionDTO.OrderId = notNullOrder;
                stockActionDTO.ProductId = id;
                stockActionDTO.StockActions = Objects.StockActionEnum.Add;
                await _stockActionService.CreateStockAction(stockActionDTO);
                await _orderService.DeleteOrderDetails(notNullOrder, id);
            }
            return RedirectToAction("ShoppingCart");
        }
    }
}
