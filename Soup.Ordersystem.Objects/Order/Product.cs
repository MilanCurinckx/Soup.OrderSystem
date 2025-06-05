using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Order
{
    [Table("OrderProducts")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string? ProductName { get; set; }        
    }
}
