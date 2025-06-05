using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Customer
{
    public class CustomerDetails
    {
        [Key]
        [ForeignKey(nameof(Customer))]
        public string CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
