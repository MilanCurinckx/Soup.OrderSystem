using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects.Customer
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public string CustomerID { get; set; }
        [ForeignKey(nameof(Address))]
        [Column("addressId")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
