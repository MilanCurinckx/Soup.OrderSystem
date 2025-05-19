using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public class AddressService()
    {
        private OrderContext _orderContext = new();
        /// <summary>
        /// Creates a new address with the given data from AddressDTO, if the given combination of street name + house number already is present in the database, it will use that one instead of making a new copy of that data. returns the created address after saving it to the db (for usage in CustomerService)
        /// </summary>
        /// <param name="addressDTO"></param>
        public async Task<Address> CreateAddress(AddressDTO addressDTO)
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
            return address;
        }
        /// <summary>
        /// Returns the address of a specific customer 
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public async Task<Address> GetAddressAsync(string CustomerId)
        {
            var address = await _orderContext.Address.Where(a => a.CustomerID == CustomerId).FirstOrDefaultAsync();
            return address;
        }
        /// <summary>
        /// returns the entire address of a given location (street name + house number) as string 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<Address> GetAddressByLocationAsync(string location)
        {
            var address = await _orderContext.Address.Where(a => a.StreetHouse == location).FirstOrDefaultAsync();
            return address;
        }
        /// <summary>
        /// returns all of the addresses as a list 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Address>> GetAddressesToListAsync()
        {
            var addresses = await _orderContext.Address.ToListAsync();
            return addresses;
        }
        /// <summary>
        /// Looks for the address that needs to be updated, and then checks if any of the values in the DTO differ from the to-be-updated object. If the values differ, the object is updated with the new values.
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <returns></returns>
        public async Task UpdateAddressAsync(AddressDTO addressDTO)
        {
            var addressToUpdate = await GetAddressAsync(addressDTO.CustomerID);
            if (addressToUpdate == null)
            {
                throw new Exception("Address could not be found");
            }
            else
            {
                //did it like this to save bandwith between memory and DB, less transfer less chance of things going wrong in the between
                if (addressToUpdate.StreetHouse == addressDTO.StreetHouse)
                {}
                else
                {
                    addressToUpdate.StreetHouse = addressDTO.StreetHouse;
                }
                if (addressToUpdate.BusNumber == addressDTO.BusNumber)
                {
                }
                else
                {
                    addressToUpdate.BusNumber = addressDTO.BusNumber;
                }
                if (addressDTO.PostalCodeId == addressDTO.PostalCodeId)
                {
                }
                else
                {
                    addressToUpdate.PostalCodeId = addressDTO.PostalCodeId;
                }
            }
            await _orderContext.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes an address 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
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
