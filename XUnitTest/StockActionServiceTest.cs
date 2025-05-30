using Soup.Ordersystem.Objects;
using Soup.Ordersystem.Objects.Order;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.XunitTests
{
    public class StockActionServiceTest
    {
        private IStockActionService _stockActionService { get; set; } = new StockActionService();
        [Fact]
        public void Test1()
        {
            StockAction stockAction = new StockAction();
            IProductService productService = new ProductService();
            int productId = productService.GetProductsList().First().ProductID;
            stockAction.ProductId = productId;
            stockAction.StockActionsEnum = StockActionEnum.Add;
            stockAction.OrderId =
            _stockActionService.CreateStockAction(stockAction);
            
        }
    }
}
