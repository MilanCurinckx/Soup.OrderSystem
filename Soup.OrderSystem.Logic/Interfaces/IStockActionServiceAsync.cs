using Soup.Ordersystem.Objects;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public interface IStockActionServiceAsync
    {
        Task CreateStockAction(StockActionDTO stockActionDTO);
        Task<int> GetAvailableStockAmount(int productId);
        Task<int> GetCurrentStockAmount(int productId);
        Task<StockAction> GetStockAction(int stockActionId);
        Task<List<StockAction>> GetStockActionsByProduct(int productId);
        Task<List<StockAction>> GetStockActionsByType(int stockActionType);
        Task<List<StockAction>> GetStockActionsList();
    }
}