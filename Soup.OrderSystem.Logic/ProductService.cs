using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Order;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    public class ProductService :IProductService
    {
        private OrderContext _orderContext = new();
        /// <summary>
        /// Creates a new product & saves it to the Db
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public async Task CreateProductAsync(ProductDTO productDTO)
        {
            Products newProduct = new();
            newProduct.ProductName = productDTO.ProductName;
            _orderContext.OrderProducts.Add(newProduct);
            await _orderContext.SaveChangesAsync();
        }
        /// <summary>
        /// Returns a single product based on the id given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Products> GetProductAsync(int id)
        {
            var product = await _orderContext.OrderProducts.Where(p => p.ProductID == id).FirstOrDefaultAsync();
            return product;
        }
        /// <summary>
        /// Returns all of the products in a List
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Products>> GetProductsListAsync()
        {
            var productList = await _orderContext.OrderProducts.ToListAsync();
            return productList;
        }
        /// <summary>
        /// Updates the product with the given data
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public async Task UpdateProductAsync(ProductDTO productDTO)
        {
            Products productToUpdate = await GetProductAsync(productDTO.ProductID);
            productToUpdate.ProductName = productDTO.ProductName;
            await _orderContext.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes the product based off the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteProductAsync(int id)
        {
            var productToDelete = GetProductAsync(id);
            if (productToDelete == null)
            {
                throw new Exception("Product couldn't be found, are you sure you have the right id?");
            }
            else
            {
                _orderContext.OrderProducts.Remove(await productToDelete);
            }
        }

    }
}
