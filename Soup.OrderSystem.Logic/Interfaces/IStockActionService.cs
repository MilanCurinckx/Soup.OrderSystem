using Soup.Ordersystem.Objects;
namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IStockActionService
    {
        void CreateStockAction(StockAction stockAction);
        int GetAvailableStockAmount(int productId);
        int GetCurrentStockAmount(int productId);
        StockAction GetStockAction(int stockActionId);
        List<StockAction> GetStockActionsByProduct(int productId);
        List<StockAction> GetStockActionsByType(int stockAction);
        List<StockAction> GetStockActionsList();
    }
}