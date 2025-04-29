using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects.Customer
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public string CustomerID { get; set; }
    }
}
