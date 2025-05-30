using Soup.Ordersystem.Objects.Order;
using Soup.Ordersystem.Objects.User;
using Soup.OrderSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.XunitTests
{

    public class ProductServiceTest
    {
        private IProductService _productService { get; set; } = new ProductService();
        [Fact]
        public void Test1()
        {
            Product product = new Product();
            product.ProductName = "Test";
            _productService.CreateProduct(product);
            Product createdProduct = _productService.GetProductsList().Last();
            Assert.NotNull(createdProduct);
        }
        [Fact]
        public void Test2()
        {
            Product product = _productService.GetProductsList().Last();
            if (product.ProductName == "Test")
            {
                product.ProductName = "updated";
            }
            else
            {
                product.ProductName = "Test";
            }
            _productService.UpdateProduct(product);
            Product updatedProduct = _productService.GetProductsList().Last();
            Assert.NotEqual(product.ProductName, updatedProduct.ProductName);
        }
        [Fact]
        public void Test3()
        {
            Product product = _productService.GetProductsList().Last();
            _productService.DeleteProduct(product.ProductID);
        }
    }
}
