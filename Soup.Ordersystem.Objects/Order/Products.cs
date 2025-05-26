using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects.Order
{
    [Table("OrderProducts")]
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }        
    }
}
