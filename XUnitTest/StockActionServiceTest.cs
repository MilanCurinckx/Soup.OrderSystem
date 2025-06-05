using Soup.OrderSystem.Objects;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System.Threading.Tasks;

namespace Soup.OrderSystem.XunitTests
{
    public class StockActionServiceTest
    {
        private IStockActionServiceAsync _stockActionService { get; set; } = new StockActionServiceAsync();
        [Fact]
        public async Task Test1()
        {
            StockActionDTO stockActionDTO = new StockActionDTO();
            StockActionDTO stockAction = new();
            StockAction createdStockAction = new StockAction();
            List<StockAction> stockActionsList = new List<StockAction>();
            IProductService productService = new ProductService();
            int productId = productService.GetProductsList().First().ProductID;
            stockAction.ProductId = productId;
            stockAction.StockActions = StockActionEnum.Add;
            stockAction.Amount = 1;
            await _stockActionService.CreateStockAction(stockAction);
            stockActionsList = await _stockActionService.GetStockActionsList();
            createdStockAction = stockActionsList.Find(x => x.ProductId == productId);
            Assert.NotNull(createdStockAction);
        }
        [Fact]
        public async Task Test2()
        {
            List<StockAction> stockActionsList = await _stockActionService.GetStockActionsList();
            StockAction stockAction = stockActionsList.First();
            StockAction getStockAction = await _stockActionService.GetStockAction(stockAction.Id);
            Assert.Equal(stockAction.ProductId, getStockAction.ProductId);
        }
        [Fact]
        public async Task Test3()
        {
            StockAction stockAction= new StockAction();
            List<StockAction> stockActions = await _stockActionService.GetStockActionsList();
            stockAction = stockActions.First();
            List<StockAction> stockActionsList = await _stockActionService.GetStockActionsByType(((int)stockAction.StockActionsEnum));
            Assert.NotNull(stockActionsList);
        }
        [Fact]
        public async Task Test4()
        {
            List<StockAction> stockActionsList = await _stockActionService.GetStockActionsList();
            StockAction stockActions = stockActionsList.First();
            List<StockAction> stockActionsProductList = await _stockActionService.GetStockActionsByProduct(stockActions.ProductId);
            Assert.NotNull(stockActionsProductList);
        }
    }
}
