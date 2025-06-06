using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.UI.Models
{
    public class CustomerModel
    {
        
        public string? CustomerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public int? AddressId { get; set; }
        [Required]
        public string StreetHouse { get; set; }
        [Range(0, int.MaxValue)]
        public int? BusNumber { get; set; }
        [Required]
        public string PostalCodeId { get; set; }
    }
}
