using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IAddressServiceAsync
    {
        Task<Address> CreateAddress(AddressDTO addressDTO);
        void DeleteAddress(int addressId);
        Task<Address> GetAddressById(int addressId);
        Task<Address> GetAddressByLocationAsync(string location);
        Task<List<Address>> GetAddressesToList();
        void UpdateAddress(AddressDTO addressDTO);
    }
}