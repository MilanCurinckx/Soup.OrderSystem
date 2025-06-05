using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    public class AddressServiceAsync : IAddressServiceAsync
    {

        /// <summary>
        /// Creates a new address with the given data from AddressDTO, if the given combination of street name + house number is present already in the database, it will use that one instead of making a new copy of that data. Same goes for postal code. Returns the created address after saving it to the db (for usage in CustomerService)
        /// </summary>
        /// <param name="addressDTO"></param>
        public async Task<Address> CreateAddress(AddressDTO addressDTO)
        {
            try
            {
                using (OrderContext context = new())
                {
                    PostalCodeService postalCodeService = new PostalCodeService();
                    Address newAddress = new();
                    //check whether this postalcode already exists in the database 
                    PostalCode? duplicatePostalCodeCheck = context.PostalCode.Where(p => p.PostalCodeID == addressDTO.PostalCodeId).FirstOrDefault();
                    if (duplicatePostalCodeCheck == null)
                    {
                        postalCodeService.CreatePostalCode(addressDTO.PostalCodeId);
                        newAddress.PostalCodeId = addressDTO.PostalCodeId;
                    }
                    else
                    {
                        newAddress.PostalCodeId = duplicatePostalCodeCheck.PostalCodeID;
                    }
                    await context.Address.AddAsync(newAddress);
                    await context.SaveChangesAsync();
                    return newAddress;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the address" + ex.Message);
            }

        }
        /// <summary>
        /// Returns the address of a specific customer 
        /// </summary>
        /// <returns></returns>
        public async Task<Address> GetAddressById(int addressId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    Address address = await context.Address.Where(a => a.AddressID == addressId).FirstAsync();
                    return address;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while getting the address" + ex.Message);
            }

        }
        /// <summary>
        /// returns the entire address of a given location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<Address> GetAddressByLocationAsync(string location)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var address = await context.Address.Where(a => a.StreetHouse == location).FirstAsync();
                    return address;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while getting the address" + ex.Message);
            }
        }

        /// <summary>
        /// returns all of the addresses as a list 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Address>> GetAddressesToList()
        {
            try
            {
                using (OrderContext context = new())
                {
                    var addresses = await context.Address.ToListAsync();
                    return addresses;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while getting the addresses" + ex.Message);
            }

        }
        /// <summary>
        /// Looks for the address that needs to be updated, and then checks if any of the values in the DTO differ from the to-be-updated object. If the values differ, the object is updated with the new values.
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <returns></returns>
        public async Task UpdateAddress(AddressDTO addressDTO)
        {
            try
            {
                var addressToUpdate = GetAddressById(addressDTO.AddressID).Result;
                using (OrderContext context = new())
                {
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
                    context.Update(addressToUpdate);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while updating the address" + ex.Message);
            }

        }
        /// <summary>
        /// Deletes an address 
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public async Task DeleteAddress(int addressId)
        {
            try
            {
                var addressToDelete = GetAddressById(addressId).Result;

                using (OrderContext context = new())
                {
                    CustomerDetails? customerCheck = await context.CustomerDetails.Where(x => x.AddressId == addressId).FirstOrDefaultAsync();
                    if (customerCheck == null)
                    {

                    }
                    else
                    {
                        CustomerService customerService = new();
                        customerService.DeleteCustomerDetails(customerCheck.CustomerID);
                    }
                    context.Address.Remove(addressToDelete);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while deleting the address" + ex.Message);
            }
        }
    }

}
