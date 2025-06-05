using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Customer
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public string CustomerId  { get; set; }
    }
}
