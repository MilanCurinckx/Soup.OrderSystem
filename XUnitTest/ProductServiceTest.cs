using Soup.Ordersystem.Objects.Order;
using Soup.Ordersystem.Objects.User;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.XunitTests
{

    public class ProductServiceTest
    {
        private IProductServiceAsync _productService { get; set; } = new ProductServiceAsync();
        [Fact]
        public async Task Test1()
        {
            ProductDTO product = new ProductDTO();
            List<Product> productsList = new List<Product>();
            Product createdProduct = new Product();
            product.ProductName = "Test";
            _productService.CreateProduct(product);
            productsList = await _productService.GetProductsList();
            createdProduct = productsList.Last();
            Assert.NotNull(createdProduct);
        }
        [Fact]
        public async Task Test2()
        {
            List<Product> productList = new List<Product>();
            List<Product> updatedProductList = new List<Product>();
            Product product = new Product();
            Product updatedProduct = new Product();
            productList = await _productService.GetProductsList();
            product = productList.Last();
            if (product.ProductName == "Test")
            {
                product.ProductName = "updated";
            }
            else
            {
                product.ProductName = "Test";
            }
            await _productService.UpdateProduct(product);
            updatedProductList = await _productService.GetProductsList();
            updatedProduct = updatedProductList.Last();
            Assert.NotEqual(product.ProductName, updatedProduct.ProductName);
        }
        [Fact]
        public async Task Test3()
        {
            List<Product> productList = new List<Product>();
            Product product = new Product();
            productList = await _productService.GetProductsList();
            product = productList.Last();
            await _productService.DeleteProduct(product.ProductID);
        }
    }
}
