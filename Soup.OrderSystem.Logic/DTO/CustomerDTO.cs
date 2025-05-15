using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.DTO
{
    public class CustomerDTO
    {
        public string CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }
        public AddressDTO AddressDTO { get; set; } = new();
    }
}
