using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects.Order;

namespace Soup.OrderSystem.UI.Controllers
{
    public class ProductController : Controller
    {
        private IProductServiceAsync _service;
        public ProductController(IProductServiceAsync service)
        {
            _service = service;
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult CreateProduct()
        {
            return View();
        }
        public async Task<IActionResult> GetProducts()
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
       
        public async Task<IActionResult> Update(int id) 
        {
            ProductDTO productDTO = new();
            Product product = await _service.GetProduct(id);
            productDTO.ProductID = product.ProductID;
            productDTO.ProductName = product.ProductName;
            return View(productDTO);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteProduct(id);
            return RedirectToAction("GetProducts");
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
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
