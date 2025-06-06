using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.Interfaces;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    public class ProductService : IProductService
    {

        /// <summary>
        /// Creates a new product & saves it to the Db
        /// </summary>
        /// <param name="products"></param>
        /// <returns></returns>
        public void CreateProduct(Product products)
        {
            try
            {
                Product newProduct = new();
                newProduct.ProductName = products.ProductName;
                using (OrderContext context = new())
                {
                    context.OrderProducts.Add(newProduct);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the product"+ex.Message);
            }
        }
        /// <summary>
        /// Returns a single product based on the id given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var product = context.OrderProducts.Where(p => p.ProductID == id).FirstOrDefault();
                    return product;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the product"+ex.Message);
            }
        }
        /// <summary>
        /// Returns all of the products in a List
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProductsList()
        {
            try
            {
                using (OrderContext context = new())
                {
                    var productList = context.OrderProducts.ToList();
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
        /// <param name="products"></param>
        /// <returns></returns>
        public void UpdateProduct(Product products)
        {
            try
            {
                Product productToUpdate = GetProduct(products.ProductID);
                using (OrderContext context = new())
                {
                    productToUpdate.ProductName = products.ProductName;
                    context.Update(productToUpdate);
                    context.SaveChanges();
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
        public void DeleteProduct(int id)
        {
            try
            {
                var productToDelete = GetProduct(id);
                using (OrderContext context = new())
                {
                    if (productToDelete == null)
                    {
                        throw new Exception("Product couldn't be found, are you sure you have the right id?");
                    }
                    else
                    {
                        context.OrderProducts.Remove(productToDelete);
                        context.SaveChanges();
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
