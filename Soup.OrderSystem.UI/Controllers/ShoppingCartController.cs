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
        /// <summary>
        /// Adds a single amount of a product to an order. Has checks whether an order has been created yet or not, and whether there's already a copy of that item in the shopping cart. if not, create a new copy of that item and add it to the shopping cart.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id)
        {
            StockActionDTO actionDTO = new StockActionDTO();
            OrderDTO orderDTO = new OrderDTO();
            OrderDetails orderDetails = new OrderDetails();
            List<OrderDetails> productsInOrder = new List<OrderDetails>();
            int amountToAdd = 1;
            string customerId = "";
            int? orderId = HttpContext.Session.GetInt32("OrderId");
            //if an order has not been created yet
            if (orderId == null)
            {   //if a customer is logged in, create a new order and save the id in the session
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
            //get the order that is created this session and create an orderdetails if it didn't exist yet
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
        /// <summary>
        /// Get every orderdetail related to this order in a list and converts them to a list of OrderProductModels to return
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ShoppingCart()
        {
            List<OrderProductModel> productModels = new List<OrderProductModel>();
            //check if an order has already been created, if not, log in to create an order
            int? orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                return RedirectToAction("CustomerLogin", "Login");
            }
            //if an order has already been created
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
        /// <summary>
        /// Adds or updates an amount of a product to the customer's shopping cart. Check in place to see if an order has already been created (required). 
        /// Check in place to see if that item has already been added to cart and requires the amount to be updated, or if it's added to the cart for the first time with a certain amount. 
        /// If checks are passed, Create a stock action to update stock. Redirects to ShoppingCart
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateAmount(OrderProductModel model)
        {
            OrderDTO orderDTO = new();
            StockActionDTO stockActionDTO = new StockActionDTO();
            OrderDetails orderDetails = new();
            string customerId = "";
            int stockCheck = 0;
            //check if there are enough items available in stock to perform this action
            stockCheck = await _stockActionService.GetAvailableStockAmount(model.ProductID);
            if (stockCheck < stockActionDTO.Amount)
            {
                ViewData["Error"] = "Not enough items in stock for this operation, please add fewer items";
                return View("Details", model);
            }

            //check if an order has been created, if not, create one if a customer is logged in.
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
            //check whether an orderdetail already exists of this item in the shopping cart or not
            orderDetails = await _orderService.GetOrderDetails(orderId: notNullOrder, productId: model.ProductID);
            //if it's the first time this item is added to a shopping cart, create a new OrderDetail to add to cart
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
            //create a new stock action to update stock
            stockActionDTO.OrderId = notNullOrder;
            stockActionDTO.ProductId = model.ProductID;
            //check whether the amount updated means that more items were taken from stock or if items were returned to stock
            //if more products were taken from stock
            if (model.ProductID > orderDetails.ProductAmount)
            {
                stockActionDTO.Amount = model.ProductAmount - orderDetails.ProductAmount;
                stockActionDTO.StockActions = Objects.StockActionEnum.Reserve;
            }
            //if items were returned to stock
            else
            {
                stockActionDTO.Amount = orderDetails.ProductAmount - model.ProductAmount;
                stockActionDTO.StockActions = Objects.StockActionEnum.Add;
            }
            await _stockActionService.CreateStockAction(stockActionDTO);
            return RedirectToAction("ShoppingCart");
        }
        /// <summary>
        /// Removes an orderdetail from an order and redirects back to the shopping cart
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
