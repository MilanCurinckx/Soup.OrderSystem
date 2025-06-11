using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.UI.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StockActionController : Controller
    {
        private IStockActionServiceAsync _stockActionService;
        private IOrderServiceAsync _orderService;
        private IProductServiceAsync _productService;
        public StockActionController(IStockActionServiceAsync stockActionService, IOrderServiceAsync orderService, IProductServiceAsync productService)
        {
            _stockActionService = stockActionService;
            _orderService = orderService;
            _productService = productService;
        }
        public async Task<IActionResult> GetStockActions()
        {
            List<StockActionDTO> stockActionDTOs = new List<StockActionDTO>();
            List<StockAction> stockActions = new List<StockAction>();
            stockActions = await _stockActionService.GetStockActionsList();
            stockActionDTOs = stockActions.Select(s => new StockActionDTO
            {
                Amount = s.Amount,
                Id = s.Id,
                ProductId = s.ProductId,
                OrderId = s.OrderId,
                StockActions = s.StockActionsEnum,
            }).ToList();
            return View(stockActionDTOs);
        }
        public async Task<IActionResult> CreateStockAction(int id)
        {
            OrderProductModel model = new OrderProductModel();
            Product product = new();
            product = await _productService.GetProduct(id);
            model.ProductID = id;
            model.ProductName = product.ProductName;
            return View(model);
        }
        public async Task<IActionResult> ManageStock(int id)
        {   
            List<StockAction> stockActionsList = new List<StockAction>();
            List<StockActionDTO> stockActionDTOs = new List<StockActionDTO>();
            stockActionsList = await _stockActionService.GetStockActionsByProduct(id);
            stockActionDTOs = stockActionsList.Select(s => new StockActionDTO
            {
                Amount = s.Amount,
                Id = s.Id,
                ProductId = s.ProductId,
                OrderId = s.OrderId,
                StockActions = s.StockActionsEnum,
            }).ToList();
            return View(stockActionDTOs);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStockAction(OrderProductModel orderProductModel)
        {
            int ? orderId = HttpContext.Session.GetInt32("OrderId");
            if (orderId == null)
            {
                
                HttpContext.Session.SetInt32("OrderId", await _orderService.CreateOrder("k1"));
                orderId = HttpContext.Session.GetInt32("OrderId");
            }
            int notNullId = orderId.Value;
            StockActionDTO stockActionDTO = new();
            stockActionDTO.Amount = orderProductModel.ProductAmount;
            stockActionDTO.ProductId = orderProductModel.ProductID;
            stockActionDTO.OrderId = notNullId;
            stockActionDTO.StockActions = orderProductModel.StockAction;
            _stockActionService.CreateStockAction(stockActionDTO);
            return RedirectToAction("Overview", "product");
        }
    }
}
