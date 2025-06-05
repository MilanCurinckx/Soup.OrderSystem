using Soup.OrderSystem.Objects.Customer;


namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IAddressService
    {
        Address CreateAddress(Address address);
        void DeleteAddress(int addressId);
        Address GetAddressById(int addressId);
        List<Address> GetAddressesToList();
        void UpdateAddress(Address address);
    }
}