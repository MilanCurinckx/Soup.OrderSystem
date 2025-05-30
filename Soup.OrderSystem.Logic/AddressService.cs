using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    public class AddressService : IAddressService
    {

        /// <summary>
        /// Creates a new address with the given data from AddressDTO, if the given combination of street name + house number is present already in the database, it will use that one instead of making a new copy of that data. Same goes for postal code. Returns the created address after saving it to the db (for usage in CustomerService)
        /// </summary>
        /// <param name="addressDTO"></param>
        public Ordersystem.Objects.Customer.Address CreateAddress(Ordersystem.Objects.Customer.Address address)
        {
            try
            {
                using (OrderContext context = new())
                {
                    PostalCodeService postalCodeService = new PostalCodeService();
                    Ordersystem.Objects.Customer.Address newAddress = new();
                    var duplicateAddressCheck = context.Address.Where(a => a.StreetHouse == address.StreetHouse).FirstOrDefault();
                    var duplicatePostalCodeCheck = context.PostalCode.Where(p => p.PostalCodeID == address.PostalCodeId).FirstOrDefault();
                    if (duplicateAddressCheck == null)
                    {
                        address.StreetHouse = address.StreetHouse;
                        address.BusNumber = address.BusNumber;
                    }
                    else
                    {
                        address.StreetHouse = duplicateAddressCheck.StreetHouse;
                        address.BusNumber = duplicateAddressCheck.BusNumber;
                    }
                    if (duplicatePostalCodeCheck == null)
                    {
                        postalCodeService.CreatePostalCode(address.PostalCodeId);
                        address.PostalCodeId = address.PostalCodeId;
                    }
                    else
                    {
                        address.PostalCodeId = duplicatePostalCodeCheck.PostalCodeID;
                    }
                    context.Address.Add(address);
                    context.SaveChanges();
                    return address;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the address");
            }

        }
        /// <summary>
        /// Returns the address of a specific customer 
        /// </summary>
        /// <returns></returns>
        public Ordersystem.Objects.Customer.Address GetAddressById(int addressId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var address = context.Address.Where(a => a.AddressID == addressId).First();
                    return address;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
        public List<Ordersystem.Objects.Customer.Address> GetAddressesToList()
        {
            try
            {
                using (OrderContext context = new())
                {
                    var addresses = context.Address.ToList();
                    return addresses;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Looks for the address that needs to be updated, and then checks if any of the values in the DTO differ from the to-be-updated object. If the values differ, the object is updated with the new values.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public void UpdateAddress(Ordersystem.Objects.Customer.Address address)
        {
            try
            {
                var addressToUpdate = GetAddressById(address.AddressID);
                using (OrderContext context = new())
                {
                    if (addressToUpdate == null)
                    {
                        throw new Exception("Address could not be found");
                    }
                    else
                    {
                        //did it like this to save bandwith between memory and DB, less transfer less chance of things going wrong in the between
                        if (addressToUpdate.StreetHouse == address.StreetHouse)
                        { }
                        else
                        {
                            addressToUpdate.StreetHouse = address.StreetHouse;
                        }
                        if (addressToUpdate.BusNumber == address.BusNumber)
                        {
                        }
                        else
                        {
                            addressToUpdate.BusNumber = address.BusNumber;
                        }
                        if (address.PostalCodeId == address.PostalCodeId)
                        {
                        }
                        else
                        {
                            addressToUpdate.PostalCodeId = address.PostalCodeId;
                        }
                    }
                    context.Update(addressToUpdate);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        /// <summary>
        /// Deletes an address 
        /// </summary>
        /// <param name="addressId"></param>
        /// <returns></returns>
        public void DeleteAddress(int addressId)
        {
            try
            {
                var addressToDelete = GetAddressById(addressId);

                using (OrderContext context = new())
                {
                    CustomerDetails customerCheck = context.CustomerDetails.Where(x => x.AddressId == addressId).FirstOrDefault();
                    if (customerCheck == null)
                    {

                    }
                    else
                    {
                        CustomerService customerService = new();
                        customerService.DeleteCustomerDetails(customerCheck.CustomerID);
                    }
                    context.Address.Remove(addressToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong while deleting the address" + e.Message);
            }
        }
    }

}
