using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductServiceAsync _productServiceAsync;
        public ProductController(IProductServiceAsync service)
        {
            _productServiceAsync = service;
        }
        [HttpPost]
        public async Task CreateProduct(ProductDTO productDTO)
        {
            await _productServiceAsync.CreateProduct(productDTO);
        }
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productServiceAsync.GetProduct(id);
            return Ok(product);
        }
        [HttpGet("GetProductsList")]
        public async Task<IActionResult> GetProductsList()
        {
            var productList = await _productServiceAsync.GetProductsList();
            return Ok(productList);
        }
        [HttpPatch]
        public async Task UpdateProduct(ProductDTO productDTO)
        {
           await _productServiceAsync.UpdateProduct(productDTO);
        }
        [HttpDelete]
        public async Task DeleteProduct(int id)
        {
            await _productServiceAsync.DeleteProduct(id);
        }
    }
}
