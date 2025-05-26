using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects.Customer
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public string CustomerId  { get; set; }
        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
