using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderServiceAsync _orderServiceAsync;
        public OrderController(IOrderServiceAsync service)
        {
            _orderServiceAsync = service;
        }
        [HttpPost]
        public async Task Create(OrderDTO orderDTO)
        {
            var Id = await _orderServiceAsync.CreateOrder(orderDTO.CustomerId);
            orderDTO.OrderID = Id;
            await _orderServiceAsync.CreateOrderDetails(orderDTO);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrder(int orderId)
        {
            var order = await _orderServiceAsync.GetOrder(orderId);
            return Ok(order);
        }
        [HttpGet("GetOrderDetailList")]
        public async Task<IActionResult> GetOrderDetailList()
        {
            var orderList = await _orderServiceAsync.GetOrderDetailsList();
            return Ok(orderList);
        }
        [HttpGet("GetOrderDetailsbyOrder")]
        public async Task<IActionResult> GetOrderDetailsbyOrder(int orderId)
        {
            var orderList = await _orderServiceAsync.GetOrderDetailsByOrder(orderId);
            return Ok(orderList);
        }
        [HttpGet("GetOrderDetailsByProduct")]
        public async Task<IActionResult> GetOrderDetailsByProduct(int productId)
        {
            var orderList = await _orderServiceAsync.GetOrderDetailsByProduct(productId);
            return Ok(orderList);
        }
        [HttpGet("GetOrderDetails")]
        public async Task<IActionResult> GetOrderDetails(int orderId, int productId)
        {
            var orderDetails = await _orderServiceAsync.GetOrderDetails(orderId, productId);
            return Ok(orderDetails);
        }
        [HttpPatch("UpdateProductAmount")]
        public async Task UpdateProductAmount(OrderDTO orderDTO)
        {
            await _orderServiceAsync.UpdateProductAmount(orderDTO);
        }
        [HttpPatch("UpdateOrderStatus")]
        public async Task UpdateOrderStatus(OrderDTO orderDTO)
        {
            await _orderServiceAsync.UpdateOrderStatus(orderDTO);
        }
        [HttpDelete]
        public async Task DeleteOrderDetails(int orderId, int productId)
        {
            await _orderServiceAsync.DeleteOrderDetails(orderId, productId);
        }
    }
}
