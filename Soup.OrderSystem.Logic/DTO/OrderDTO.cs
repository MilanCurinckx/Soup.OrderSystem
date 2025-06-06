using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Objects.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Logic.DTO
{
    public class OrderDTO
    {   
        public string CustomerId { get; set; }
        public int OrderID { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public int ProductID { get; set; }
        [Range(0, int.MaxValue)]
        public int ProductAmount { get; set; }
    }
}