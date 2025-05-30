using Soup.Ordersystem.Objects.Order;

namespace Soup.OrderSystem.Logic
{
    public interface IProductService
    {
        void CreateProduct(Product products);
        void DeleteProduct(int id);
        Product GetProduct(int id);
        List<Product> GetProductsList();
        void UpdateProduct(Product products);
    }
}