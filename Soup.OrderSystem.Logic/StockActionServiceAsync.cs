using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    public class StockActionServiceAsync : IStockActionServiceAsync
    {

        /// <summary>
        /// Creates a new stock action and saves it to the db
        /// </summary>newStockAction
        /// <param name="stockActionDTO"></param>
        /// <returns></returns>
        public async Task CreateStockAction(StockActionDTO stockActionDTO)
        {
            try
            {
                using (OrderContext context = new())
                {
                    StockAction newStockAction = new();
                    newStockAction.Id = stockActionDTO.Id;
                    newStockAction.Amount = stockActionDTO.Amount;
                    newStockAction.ProductId = stockActionDTO.ProductId;
                    newStockAction.StockActionsEnum = stockActionDTO.StockActions;
                    context.Add(newStockAction);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the stock action " + ex.Message);
            }
        }

        /// <summary>
        ///  returns the stock action corresponding to the given id, if not found it will return null
        /// </summary>
        /// <param name="stockActionId"></param>
        /// <returns></returns>
        public async Task<StockAction> GetStockAction(int stockActionId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var stockAction = await context.Stock_Actions.Where(s => s.Id == stockActionId).FirstOrDefaultAsync();
                    return stockAction;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the stock action " + ex.Message);
            }
        }
        /// <summary>
        /// returns a list of all the stock actions in the db
        /// </summary>
        /// <returns></returns>
        public async Task<List<StockAction>> GetStockActionsList()
        {
            try
            {
                using (OrderContext context = new())
                {
                    var stockActions = await context.Stock_Actions.ToListAsync();
                    return stockActions;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the stock actions " + ex.Message);
            }
        }

        /// <summary>
        /// returns a list of stock actions depending on what value has been passed along. 1 = all the stock actions that add stock. 2 = all the stock actions that remove stock. 3 = all the stock actions that reserve stock.
        /// </summary>
        /// <param name="stockAction"></param>
        /// <returns></returns>
        public async Task<List<StockAction>> GetStockActionsByType(int stockAction)
        {
            try
            {
                using (OrderContext context = new())
                {
                    List<StockAction> stockActionsList = new();
                    switch (stockAction)
                    {
                        case (int)StockActionEnum.Add:
                            stockActionsList = await context.Stock_Actions.Where(s => s.StockActionsEnum == StockActionEnum.Add).ToListAsync();
                            break;
                        case (int)StockActionEnum.Remove:
                            stockActionsList = await context.Stock_Actions.Where(s => s.StockActionsEnum == StockActionEnum.Remove).ToListAsync();
                            break;
                        case (int)StockActionEnum.Reserve:
                            stockActionsList = await context.Stock_Actions.Where(s => s.StockActionsEnum == StockActionEnum.Reserve).ToListAsync();
                            break;
                    }
                    return stockActionsList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the stock actions " + ex.Message);
            }
        }
        /// <summary>
        /// returns a list of all the stock actions for a specific product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<List<StockAction>> GetStockActionsByProduct(int productId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var stockActions = await context.Stock_Actions.Where(s => s.ProductId == productId).ToListAsync();
                    return stockActions;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the stock actions " + ex.Message);
            }
        }
        /// <summary>
        /// Returns the current stock amount of an item. First gets a list of all the stock actions of that product, and then puts all of the add and remove stock actions in separate lists. After that the sum of the stock action amount is calculated for the added & removed lists respectively. The end result is the sum of the of the added amount minus the sum of the removed amount. 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<int> GetCurrentStockAmount(int productId)
        {
            var productStockActions = await GetStockActionsByProduct(productId);
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
                    if (stockAction.StockActionsEnum == StockActionEnum.Add)
                    {
                        stockAddedList.Add(stockAction);
                    }
                    if (stockAction.StockActionsEnum == StockActionEnum.Remove)
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
        public async Task<int> GetAvailableStockAmount(int productId)
        {
            var productStockActions = await GetStockActionsByProduct(productId);
            var currentStockAmount = await GetCurrentStockAmount(productId);
            var reservedStockList = new List<StockAction>();
            int reservedStockAmount = 0;
            int availableStockAmount = 0;
            if (productStockActions == null)
            { }
            else
            {
                foreach (var stockAction in productStockActions)
                {
                    if (stockAction.StockActionsEnum == StockActionEnum.Reserve)
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
