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
            stockAction.Amount = 1;
            _stockActionService.CreateStockAction(stockAction);
            StockAction createdStockAction = _stockActionService.GetStockActionsList().Find(x => x.ProductId == productId);
            Assert.NotNull(createdStockAction);
        }
        [Fact]
        public void Test2()
        {
            List<StockAction> stockActionsList = _stockActionService.GetStockActionsList();
            StockAction stockAction = stockActionsList.First();
            StockAction getStockAction = _stockActionService.GetStockAction(stockAction.Id);
            Assert.Equal(stockAction.ProductId, getStockAction.ProductId);
        }
        [Fact]
        public void Test3()
        {
            StockAction stockAction= new StockAction();
            List<StockAction> stockActions = _stockActionService.GetStockActionsList();
            stockAction = stockActions.First();
            List<StockAction> stockActionsList = _stockActionService.GetStockActionsByType(((int)stockAction.StockActionsEnum));
            Assert.NotNull(stockActionsList);
        }
        [Fact]
        public void Test4()
        {
            List<StockAction> stockActionsList = _stockActionService.GetStockActionsList();
            StockAction stockActions = stockActionsList.First();
            List<StockAction> stockActionsProductList = _stockActionService.GetStockActionsByProduct(stockActions.ProductId);
            Assert.NotNull(stockActionsProductList);
        }
    }
}
