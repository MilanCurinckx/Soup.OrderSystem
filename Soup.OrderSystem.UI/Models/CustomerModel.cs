using Soup.OrderSystem.Logic.DTO;
using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.UI.Models
{
    public class CustomerModel : CustomerDTO
    {
        
        public int? AddressId { get; set; }
        [Required]
        public string StreetHouse { get; set; }
        [Range(0, int.MaxValue)]
        public int? BusNumber { get; set; }
        [Required]
        public string PostalCodeId { get; set; }
    }
}
