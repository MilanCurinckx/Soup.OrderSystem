using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IAddressService
    {
        Task<Address> CreateAddress(AddressDTO addressDTO);
        Task DeleteAddressAsync(int addressId);
        Task<Address> GetAddressByIdAsync(int addressId);
        //Task<Address> GetAddressByLocationAsync(string location);
        Task<IEnumerable<Address>> GetAddressesToListAsync();
        Task UpdateAddressAsync(AddressDTO addressDTO);
    }
}