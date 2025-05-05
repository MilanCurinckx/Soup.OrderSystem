using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public class CustomerDTO
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressId { get; set; }
        public AddressDTO AddressDTO { get; set; }
    }
}
