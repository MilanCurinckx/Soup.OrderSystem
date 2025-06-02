using Microsoft.AspNetCore.Mvc;
using Soup.Ordersystem.Objects;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockActionController : Controller
    {
        private readonly IStockActionServiceAsync _stockActionServiceAsync;
        public StockActionController(IStockActionServiceAsync service)
        {
            _stockActionServiceAsync = service;
        }
        [HttpPost]
        public async Task CreateStockAction(StockActionDTO stockActionDTO)
        {
           await _stockActionServiceAsync.CreateStockAction(stockActionDTO);
        }
        [HttpGet("GetAvailableStockAmount")]
        public async Task<IActionResult> GetAvailableStockAmount(int productId)
        {
            var stockAmount = await _stockActionServiceAsync.GetAvailableStockAmount(productId);
            return Ok(stockAmount); 
        }
        [HttpGet("GetCurrentStockAmount")]
        public async Task<IActionResult> GetCurrentStockAmount(int productId)
        {
            var stockAmount = await _stockActionServiceAsync.GetCurrentStockAmount(productId);
            return Ok(stockAmount);
        }
        [HttpGet("GetStockAction")]
        public async Task<IActionResult> GetStockAction(int stockActionId)
        {
            var stockAction = await _stockActionServiceAsync.GetStockAction(stockActionId);
            return Ok(stockAction);
        }
        [HttpGet("GetStockActionsByProduct")]
        public async Task<IActionResult> GetStockActionsByProduct(int productId)
        {
            var stockAction = await _stockActionServiceAsync.GetStockActionsByProduct(productId);
            return Ok(stockAction);
        }
        [HttpGet("GetStockActionsByType")]
        public async Task<IActionResult> GetStockActionsByType(int stockActionType)
        {
            var stockAction = await _stockActionServiceAsync.GetStockActionsByType(stockActionType);
            return Ok(stockAction);
        }
        [HttpGet("GetStockActionsList")]
        public async Task<IActionResult> GetStockActionsList()
        {
            var stockAction = await _stockActionServiceAsync.GetStockActionsList();
            return Ok(stockAction);
        }
    }
}
