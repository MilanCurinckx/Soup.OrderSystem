using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IAddressService
    {
        Task<Address> CreateAddress(AddressDTO addressDTO);
        Task DeleteAddressAsync(string customerId);
        Task<Address> GetAddressAsync(string CustomerId);
        Task<Address> GetAddressByLocationAsync(string location);
        Task<IEnumerable<Address>> GetAddressesToListAsync();
        Task UpdateAddressAsync(AddressDTO addressDTO);
    }
}