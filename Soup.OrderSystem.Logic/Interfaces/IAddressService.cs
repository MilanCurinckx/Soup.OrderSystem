using Soup.Ordersystem.Objects.Customer;


namespace Soup.OrderSystem.Logic
{
    public interface IAddressService
    {
        Ordersystem.Objects.Customer.Address CreateAddress(Ordersystem.Objects.Customer.Address address);
        void DeleteAddress(int addressId);
        Ordersystem.Objects.Customer.Address GetAddressById(int addressId);
        List<Ordersystem.Objects.Customer.Address> GetAddressesToList();
        void UpdateAddress(Ordersystem.Objects.Customer.Address address);
    }
}