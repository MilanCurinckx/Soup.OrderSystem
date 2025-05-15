using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public interface IAddressService
    {
        Task<Address> CreateAddress(AddressDTO addressDTO);
        Task DeleteAddressAsync(string customerId);
        Task<Address> GetAddressAsync(string CustomerId);
        Task<Address> GetAddressByLocationAsync(string location);
        Task UpdateAddressAsync(AddressDTO addressDTO);
    }
}