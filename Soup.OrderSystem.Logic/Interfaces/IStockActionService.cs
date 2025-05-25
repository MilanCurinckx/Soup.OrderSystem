using Soup.Ordersystem.Objects;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IStockActionService
    {
        Task CreateStockAction(StockActionDTO stockActionDTO);
        Task<int> GetAvailableStockAmountAsync(int productId);
        Task<int> GetCurrentStockAmountAsync(int productId);
        Task<StockAction> GetStockActionAsync(int stockActionId);
        Task<IEnumerable<StockAction>> GetStockActionsAsyncByType(int stockAction);
        Task<IEnumerable<StockAction>> GetStockActionsByProductAsync(int productId);
        Task<IEnumerable<StockAction>> GetStockActionsListAsync();
    }
}