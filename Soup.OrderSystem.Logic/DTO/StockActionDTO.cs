using Soup.OrderSystem.Objects;
using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.Logic.DTO
{
    public class StockActionDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public StockActionEnum StockActions { get; set; }
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
        public int OrderId { get; set; }
    }
}