using Soup.OrderSystem.Objects.Customer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Order
{
    //This object has the heading information of the order
    public class OrderId
    {
        [Key]
        public int Order_Id { get; set; }
        [ForeignKey("Customer_Id")]
        public int Customer_Id { get; set; }

    }
}