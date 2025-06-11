using Soup.OrderSystem.Objects;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.Interfaces
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
        Task<List<StockAction>> GetStockActionByOrder(int OrderId);
        Task DeleteStockAction(int stockActionId);
    }
}