using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public class StockActionService
    {
        private OrderContext _orderContext = new();
        public async Task CreateStockAction(StockActionDTO stockActionDTO)
        {
            StockAction stockAction = new();
            stockAction.Id = stockActionDTO.Id;
            stockAction.Amount = stockActionDTO.Amount;
            stockAction.ProductId = stockActionDTO.ProductId;
            stockAction.StockActions = stockAction.StockActions;
            stockAction.OrderId = stockActionDTO.OrderId;
            _orderContext.Add(stockAction);
            await _orderContext.SaveChangesAsync();
        }
        public async Task<StockAction> GetStockActionAsync(int stockActionId)
        {
            var stockAction = await _orderContext.Stock_Actions.Where(s => s.Id == stockActionId).FirstOrDefaultAsync();
            return stockAction;
        }
        public async Task<IEnumerable<StockAction>> GetStockActionsListAsync()
        {
            var stockActions = await _orderContext.Stock_Actions.ToListAsync();
            return stockActions;
        }
        public async Task<IEnumerable<StockAction>>GetStockActionsAsyncByType(int stockAction)
        {
            List<StockAction> stockActionsList = new();
            switch (stockAction)
            {
                case (int)StockActionEnum.Add:
                    stockActionsList = await _orderContext.Stock_Actions.Where(s=> s.StockActions == StockActionEnum.Add).ToListAsync(); 
                    break;
                case (int)StockActionEnum.Remove:
                    stockActionsList = await _orderContext.Stock_Actions.Where(s => s.StockActions == StockActionEnum.Remove).ToListAsync();
                    break;
                case(int)StockActionEnum.Reserve:
                    stockActionsList
            }
        }
        //public async Task UpdateStockActionAsync(StockActionDTO stockActionDTO)
        //{
        //    StockAction stockActionToUpdate = await GetStockActionAsync(stockActionDTO.Id);
        //    if (stockActionToUpdate == null)
        //    {
        //        throw new Exception("Stock action could not be found");
        //    }
        //    else
        //    {
        //        //for reasoning on why it is coded like this, check out UpdateAddressAsync
        //        if (stockActionToUpdate.ProductId == stockActionDTO.ProductId)
        //        {
        //        }
        //        else
        //        {
        //            stockActionToUpdate.ProductId = stockActionDTO.ProductId;
        //        }
        //        if (stockActionToUpdate.Amount == stockActionDTO.Amount)
        //        { }
        //        else
        //        {
        //            stockActionToUpdate.Amount = stockActionDTO.Amount;
        //        }
        //        if (stockActionToUpdate.OrderId == stockActionDTO.OrderId)
        //        { }
        //        else
        //        {
        //            stockActionToUpdate.OrderId = stockActionDTO.OrderId;
        //        }
        //        //can't compare an enum to an int, so I have to check this on a switch case basis
        //        switch (stockActionDTO.StockActions)
        //        {
        //            case (int) StockActionEnum.Add:
        //                stockActionToUpdate.StockActions = StockActionEnum.Add; 
        //                break;
        //            case (int) StockActionEnum.Remove:
        //                stockActionToUpdate.StockActions = StockActionEnum.Remove;
        //                break;
        //            case (int) StockActionEnum.Reserve:
        //                stockActionToUpdate.StockActions = StockActionEnum.Reserve;
        //                break;
        //        }
        //        await _orderContext.SaveChangesAsync();
        //    }
        //}
        //public async Task DeleteStockAction(int stockActionId)
        //{
        //    var stockActionToDelete = await GetStockActionAsync(stockActionId);
        //    if (stockActionToDelete == null)
        //    {
        //        throw new Exception("Stock action could not be found");
        //    }
        //    else
        //    {
        //        _orderContext.Stock_Actions.Remove(stockActionToDelete);
        //        await _orderContext.SaveChangesAsync();
        //    }
        //}
    }
}
