using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Order;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    public class ProductServiceAsync : IProductServiceAsync
    {

        /// <summary>
        /// Creates a new product & saves it to the Db
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public async Task CreateProduct(ProductDTO productDTO)
        {
            try
            {
                Product newProduct = new();
                newProduct.ProductName = productDTO.ProductName;
                using (OrderContext context = new())
                {
                    context.OrderProducts.Add(newProduct);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the product" + ex.Message);
            }
        }
        /// <summary>
        /// Returns a single product based on the id given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProduct(int id)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var product = await context.OrderProducts.Where(p => p.ProductID == id).FirstOrDefaultAsync();
                    return product;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the product" + ex.Message);
            }
        }
        /// <summary>
        /// Returns all of the products in a List
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetProductsList()
        {
            try
            {
                using (OrderContext context = new())
                {
                    var productList = await context.OrderProducts.ToListAsync();
                    return productList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the products" + ex.Message);
            }
        }
        /// <summary>
        /// Updates the product with the given data
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        public async Task UpdateProduct(ProductDTO productDTO)
        {
            try
            {
                Product productToUpdate = await GetProduct(productDTO.ProductID);
                using (OrderContext context = new())
                {
                    productToUpdate.ProductName = productDTO.ProductName;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while updating the product" + ex.Message);
            }
        }
        /// <summary>
        /// Deletes the product based off the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await GetProduct(id);
                using (OrderContext context = new())
                {
                    if (productToDelete == null)
                    {
                        throw new Exception("Product couldn't be found, are you sure you have the right id?");
                    }
                    else
                    {
                        context.OrderProducts.Remove(productToDelete);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while deleting the product" + ex.Message);
            }
        }
    }
}
