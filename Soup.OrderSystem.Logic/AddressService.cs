using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public class AddressService()
    {
        private OrderContext _orderContext = new();
        public async Task CreateAddress(AddressDTO addressDTO)
        {
            Address address = new();
            var duplicateAddressCheck = await GetAddressByLocationAsync(addressDTO.StreetHouse);
            if (duplicateAddressCheck == null)
            {
                address.StreetHouse = addressDTO.StreetHouse;
                address.BusNumber = addressDTO.BusNumber;
            }
            else
            {
                address.StreetHouse = duplicateAddressCheck.StreetHouse;
                address.BusNumber = duplicateAddressCheck.BusNumber;
            }
            address.CustomerID = addressDTO.CustomerID;
            address.PostalCodeId = addressDTO.PostalCodeId;
            _orderContext.Address.Add(address);
            await _orderContext.SaveChangesAsync();
        }
        public async Task<Address> GetAddressAsync(string CustomerId)
        {
            var address = await _orderContext.Address.Where(a => a.CustomerID == CustomerId).FirstOrDefaultAsync();
            return address;
        }
        public async Task<Address> GetAddressByLocationAsync(string location)
        {
            var address = await _orderContext.Address.Where(a => a.StreetHouse == location).FirstOrDefaultAsync();
            return address;
        }
        public async Task UpdateAddressAsync(AddressDTO addressDTO)
        {
            var addressToUpdate = await GetAddressAsync(addressDTO.CustomerID);
            if (addressToUpdate.StreetHouse != addressDTO.StreetHouse)
            {
                addressToUpdate.StreetHouse = addressDTO.StreetHouse;
            }
            if (addressToUpdate.BusNumber != addressDTO.BusNumber)
            {
                addressToUpdate.BusNumber = addressDTO.BusNumber;
            }
            if (addressDTO.PostalCodeId != addressDTO.PostalCodeId)
            {
                addressToUpdate.PostalCodeId = addressDTO.PostalCodeId;
            }
            await _orderContext.SaveChangesAsync();
        }
        public async Task DeleteAddressAsync(string customerId)
        {
            var addressToDelete = await GetAddressAsync(customerId);
            if (addressToDelete != null)
            {
                _orderContext.Address.Remove(addressToDelete);
                await _orderContext.SaveChangesAsync();
            }

        }
    }
}
