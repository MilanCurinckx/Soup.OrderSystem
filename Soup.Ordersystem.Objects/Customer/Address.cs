using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects.Customer
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }
        [ForeignKey(nameof(Customer))]
        public string CustomerID { get; set; }
        public string StreetHouse { get; set; }
        public int BusNumber { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
