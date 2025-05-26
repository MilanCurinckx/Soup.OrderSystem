using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects.Customer
{
    public class Address
    {
        [Key]
        [Required]
        public int AddressID { get; set; }
        public string? StreetHouse { get; set; }
        public int? BusNumber { get; set; }
        [ForeignKey(nameof(PostalCode))]
        [Column("PostalCode")]
        [Required]
        public string PostalCodeId {  get; set; }
        public virtual PostalCode PostalCode {  get; set; }
      
    }
}
