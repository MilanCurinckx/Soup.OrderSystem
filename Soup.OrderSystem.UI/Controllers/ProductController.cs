using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.UI.Models;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Controllers
{

    public class ProductController : Controller
    {
        private IProductServiceAsync _service;
        private IStockActionServiceAsync _actionService;
        
        public ProductController(IProductServiceAsync service, IStockActionServiceAsync actionService)
        {
            _service = service;
            _actionService = actionService;
        }
        public IActionResult Products()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            OrderProductModel orderProductModel = new();
            Product product = new();
            product = await _service.GetProduct(id);
            int currentStockAmount = await _actionService.GetCurrentStockAmount(product.ProductID);
            int availableStockAmount = await _actionService.GetAvailableStockAmount(product.ProductID);
            orderProductModel.ProductName = product.ProductName;
            orderProductModel.ProductID = id;
            orderProductModel.AmountInStock = currentStockAmount;
            orderProductModel.AvailableStock = availableStockAmount;
            return View(orderProductModel);
        }
        public async Task<IActionResult> Overview()
        {
            List<ProductDTO> productDTOs = new List<ProductDTO>();
            List<Product> ProductList = await _service.GetProductsList();
            productDTOs = ProductList.Select(p => new ProductDTO
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName
            }
            ).ToList();
            return View(productDTOs);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProducts()
        {
            List<OrderProductModel> productDTOs = new List<OrderProductModel>();
            List<Product> ProductList = await _service.GetProductsList();
            int currentStockAmount = 0;
            int availableStockAmount = 0;
            foreach (Product product in ProductList)
            {
                currentStockAmount = await _actionService.GetCurrentStockAmount(product.ProductID);
                availableStockAmount = await _actionService.GetAvailableStockAmount(product.ProductID);
                OrderProductModel orderProductModel = new OrderProductModel();
                orderProductModel.ProductID = product.ProductID;
                orderProductModel.ProductName = product.ProductName;
                orderProductModel.AmountInStock = currentStockAmount;
                orderProductModel.AvailableStock = availableStockAmount;
                productDTOs.Add(orderProductModel);
            }
            return View(productDTOs);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            ProductDTO productDTO = new();
            Product product = await _service.GetProduct(id);
            productDTO.ProductID = product.ProductID;
            productDTO.ProductName = product.ProductName;
            return View(productDTO);
        }
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteProduct(id);
            return RedirectToAction("GetProducts");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateProduct(productDTO);
                return RedirectToAction("GetProducts");
            }
            else
            {
                return View(productDTO);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO ProductDTO)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateProduct(ProductDTO);
                return RedirectToAction("GetProducts");
            }
            else
            {
                return View(ProductDTO);
            }
        }

    }
}
