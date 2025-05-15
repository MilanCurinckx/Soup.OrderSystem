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
        [Column("addressId")]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        //ctor to concat the customerId properly
        public Customer(int customerId)
        {  
            CustomerId = string.Concat('k'+customerId);
        }
    }
}
