using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Objects;
using Soup.OrderSystem.Objects.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Models
{
    public class OrderProductModel : ProductDTO
    {
        [Required]
        [Range(1,999)]
        public int ProductAmount { get; set; }
        public int AvailableStock { get; set; }
        public int AmountInStock { get; set; }
        public int OrderId { get; set; }
        public string CustomerId {  get; set; }
        [Required]
        public StockActionEnum StockAction { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
