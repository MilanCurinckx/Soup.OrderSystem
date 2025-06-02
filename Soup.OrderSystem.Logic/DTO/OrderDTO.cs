using Soup.Ordersystem.Objects.Customer;
using Soup.Ordersystem.Objects.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Logic.DTO
{
    public class OrderDTO
    {   
        public string CustomerId { get; set; }
        public int OrderID { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public int ProductID { get; set; }
        public int ProductAmount { get; set; }
    }
}