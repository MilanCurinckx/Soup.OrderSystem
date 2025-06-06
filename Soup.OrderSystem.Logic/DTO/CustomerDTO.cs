using Soup.OrderSystem.Logic.DTO;
using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.Logic.DTO
{
    public class CustomerDTO
    {
        public string CustomerID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int AddressId { get; set; }
        
    }
}
