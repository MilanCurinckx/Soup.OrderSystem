using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    public class StockActionService : IStockActionService
    {
        private OrderContext _orderContext = new();
        /// <summary>
        /// Creates a new stock action and saves it to the db
        /// </summary>
        /// <param name="stockActionDTO"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  returns the stock action corresponding to the given id, if not found it will return null
        /// </summary>
        /// <param name="stockActionId"></param>
        /// <returns></returns>
        public async Task<StockAction> GetStockActionAsync(int stockActionId)
        {
            var stockAction = await _orderContext.Stock_Actions.Where(s => s.Id == stockActionId).FirstOrDefaultAsync();
            return stockAction;
        }
        /// <summary>
        /// returns a list of all the stock actions in the db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StockAction>> GetStockActionsListAsync()
        {
            var stockActions = await _orderContext.Stock_Actions.ToListAsync();
            return stockActions;
        }

        /// <summary>
        /// returns a list of stock actions depending on what value has been passed along. 1 = all the stock actions that add stock. 2 = all the stock actions that remove stock. 3 = all the stock actions that reserve stock.
        /// </summary>
        /// <param name="stockAction"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StockAction>> GetStockActionsAsyncByType(int stockAction)
        {
            List<StockAction> stockActionsList = new();
            switch (stockAction)
            {
                case (int)StockActionEnum.Add:
                    stockActionsList = await _orderContext.Stock_Actions.Where(s => s.StockActions == StockActionEnum.Add).ToListAsync();
                    break;
                case (int)StockActionEnum.Remove:
                    stockActionsList = await _orderContext.Stock_Actions.Where(s => s.StockActions == StockActionEnum.Remove).ToListAsync();
                    break;
                case (int)StockActionEnum.Reserve:
                    stockActionsList = await _orderContext.Stock_Actions.Where(s => s.StockActions == StockActionEnum.Reserve).ToListAsync();
                    break;
            }
            return stockActionsList;
        }
        /// <summary>
        /// returns a list of all the stock actions for a specific product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StockAction>> GetStockActionsByProductAsync(int productId)
        {
            var stockActions = await _orderContext.Stock_Actions.Where(s => s.ProductId == productId).ToListAsync();
            return stockActions;
        }
        /// <summary>
        /// Returns the current stock amount of an item. First gets a list of all the stock actions of that product, and then puts all of the add and remove stock actions in separate lists. After that the sum of the stock action amount is calculated for the added & removed lists respectively. The end result is the sum of the of the added amount minus the sum of the removed amount. 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int> GetCurrentStockAmountAsync(int productId)
        {
            var productStockActions = await GetStockActionsByProductAsync(productId);
            var stockAddedList = new List<StockAction>();
            int stockAddedTotal = 0;
            var stockRemovedList = new List<StockAction>();
            int stockRemovedTotal = 0;
            int CurrentstockAmount = 0;
            if (productStockActions == null)
            { }
            else
            {
                foreach (var stockAction in productStockActions)
                {
                    if (stockAction.StockActions == StockActionEnum.Add)
                    {
                        stockAddedList.Add(stockAction);
                    }
                    if (stockAction.StockActions == StockActionEnum.Remove)
                    {
                        stockRemovedList.Add(stockAction);
                    }
                }
                stockAddedTotal = stockAddedList.Sum(s => s.Amount);
                stockRemovedTotal = stockRemovedList.Sum(s => s.Amount);
                CurrentstockAmount = stockAddedTotal - stockRemovedTotal;
            }
            return CurrentstockAmount;
        }
        /// <summary>
        /// Returns the available stock amount of an item.
        /// Gets a list of all the stock actions of a product and puts all of the reserved actions of that product in a separate list. Take the sum of all the amount in the reserved list and subtract it from the value gotten from GetCurrentStockAmount.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int> GetAvailableStockAmountAsync(int productId)
        {
            var productStockActions = await GetStockActionsByProductAsync(productId);
            var currentStockAmount = await GetCurrentStockAmountAsync(productId);
            var reservedStockList = new List<StockAction>();
            int reservedStockAmount = 0;
            int availableStockAmount = 0;
            if (productStockActions == null)
            { }
            else
            {
                foreach (var stockAction in productStockActions)
                {
                    if (stockAction.StockActions == StockActionEnum.Reserve)
                    {
                        reservedStockList.Add(stockAction);
                    }
                }
                reservedStockAmount = reservedStockList.Sum(s => s.Amount);
                availableStockAmount = currentStockAmount - reservedStockAmount;
            }
            return availableStockAmount;

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
