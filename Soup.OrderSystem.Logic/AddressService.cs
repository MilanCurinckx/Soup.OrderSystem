using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    public class AddressService() : IAddressService
    {
        private OrderContext _orderContext = new();
        /// <summary>
        /// Creates a new address with the given data from AddressDTO, if the given combination of street name + house number is present already in the database, it will use that one instead of making a new copy of that data. Same goes for postal code. Returns the created address after saving it to the db (for usage in CustomerService)
        /// </summary>
        /// <param name="addressDTO"></param>
        public async Task<Address> CreateAddress(AddressDTO addressDTO)
        {
            PostalCodeService postalCodeService = new PostalCodeService();
            Address address = new();
            var duplicateAddressCheck = _orderContext.Address.Where(a => a.StreetHouse == addressDTO.StreetHouse).FirstOrDefault();
            var duplicatePostalCodeCheck = _orderContext.PostalCode.Where(p => p.PostalCodeID == addressDTO.PostalCodeId).FirstOrDefault();
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
            if (duplicatePostalCodeCheck == null)
            {
                await postalCodeService.CreatePostalCodeAsync(addressDTO.PostalCodeId);
                address.PostalCodeId = addressDTO.PostalCodeId;
            }
            else
            {
                address.PostalCodeId = duplicatePostalCodeCheck.PostalCodeID;
            }
            _orderContext.Address.Add(address);
            await _orderContext.SaveChangesAsync();
            return address;
        }
        /// <summary>
        /// Returns the address of a specific customer 
        /// </summary>
        /// <returns></returns>
        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            var address = await _orderContext.Address.Where(a => a.AddressID == addressId).FirstAsync();
            return address;
        }
        ///// <summary>
        ///// returns the entire address of a given location
        ///// </summary>
        ///// <param name="location"></param>
        ///// <returns></returns>
        //public async Task<Address> GetAddressByLocationAsync(string location)
        //{
        //    var address = await _orderContext.Address.Where(a => a.StreetHouse == location).FirstAsync();
        //    return address;
        //}

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
            var addressToUpdate = await GetAddressByIdAsync(addressDTO.AddressID);
            if (addressToUpdate == null)
            {
                throw new Exception("Address could not be found");
            }
            else
            {
                //did it like this to save bandwith between memory and DB, less transfer less chance of things going wrong in the between
                if (addressToUpdate.StreetHouse == addressDTO.StreetHouse)
                { }
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
        public async Task DeleteAddressAsync(int addressId)
        {
            var addressToDelete = await GetAddressByIdAsync(addressId);
            if (addressToDelete == null)
            {
            }
            else 
            {
                _orderContext.Address.Remove(addressToDelete);
                await _orderContext.SaveChangesAsync();
            }
        }
    }

}
