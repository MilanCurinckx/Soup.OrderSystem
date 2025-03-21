using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Order
{
    public enum Status
    {
        New = 0,
        Delivered,
        Canceled
    }
    public class OrderDetails 
    {
        [ForeignKey("Order_Id")]
        public int Order_Id { get; set; }
        [ForeignKey("Product_Id")]
        public int Product_Id { get; set; }
        public int ProductAmount { get; set; }
        //putting the status in an enum for easy access & saving to a db
        public Status OrderStatus { get; set; }
    }
}