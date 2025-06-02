using Soup.Ordersystem.Objects;

namespace Soup.OrderSystem.Logic.DTO
{
    public class StockActionDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public StockActionEnum StockActions { get; set; }
        public int Amount { get; set; }
        public int OrderId { get; set; }
    }
}