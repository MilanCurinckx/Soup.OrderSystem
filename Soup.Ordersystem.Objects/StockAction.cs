using Soup.Ordersystem.Objects.Order;

namespace Soup.Ordersystem.Objects
{
    public class StockAction
    {
        public Products Product { get; set; }
        public StockActionEnum ActionEnum { get; set; }
        public int Amount { get; set; }
        public enum StockActionEnum
        {
            Add = 1,
            Remove = 2,
            Reserve = 3,
        }
    }
}
