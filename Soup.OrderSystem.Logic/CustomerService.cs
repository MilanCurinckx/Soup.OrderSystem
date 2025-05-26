using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    public class CustomerService : ICustomerService
    {
        private OrderContext _context = new();
        private IAddressService _addressService = new AddressService();

        /// <summary>
        /// generates a new customerId by looking at the last one in the DB and increasing that by one. this would've been done by EF except for the fact that it's not complete without a 'k' added in front. 
        /// </summary>
        /// <returns></returns>
        public async Task<int> CreateCustomerID()
        {
            var customerList = await GetCustomersAsync();
            var latestCustomer = customerList.LastOrDefault().CustomerId;
            latestCustomer = latestCustomer.Substring(1);
            int newCustomerId = int.Parse(latestCustomer);
            newCustomerId++;
            return newCustomerId;
        }
        /// <summary>
        /// Creates a new customer to be added, calls upon CreateCustomerId to create a new Id which afterwards gets the 'k' char concatenated. Also immediately creates a customerDetails which is linked to CustomerId & calls on addressService to create a new Address for the customer (unless the address already exists => look at addressservice for that)
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        public async Task CreateCustomer(CustomerDTO customerDTO)
        {
            Address newAddress = await _addressService.CreateAddress(customerDTO.AddressDTO);
            string customerId = CreateCustomerID().Result.ToString();
            Customer customer = new();
            var id = string.Concat('k' + customerId);
            customer.CustomerId = id;
            customer.AddressId = newAddress.AddressID;
            _context.Add(customer);
            await _context.SaveChangesAsync();
            CustomerDetails customerDetails = new();
            customerDetails.CustomerID = customer.CustomerId;
            customerDetails.FirstName = customerDTO.FirstName;
            customerDetails.LastName = customerDTO.LastName;
            customerDetails.Email = customerDTO.Email;
            _context.Add(customerDetails);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// returns a customer by their id, use this if you want to know what addressId is tied to this customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            var customer = await _context.Customer.Where(c => c.CustomerId == customerId).FirstOrDefaultAsync();
            return customer;
        }
        /// <summary>
        /// returns a list of every customer in the Customer table. This does NOT returns their details, only Id and and addressId for each. Use GetCustomerDetailsList for more details on each customer
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customer = await _context.Customer.ToListAsync();
            return customer;
        }
        /// <summary>
        /// returns the customerdetails of a specific customer by their id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<CustomerDetails> GetCustomerDetailsAsync(string customerId)
        {
            var customerDetails = await _context.CustomerDetails.Where(c => c.CustomerID == customerId).FirstOrDefaultAsync();
            return customerDetails;
        }
        /// <summary>
        /// return a list of overy customerdetails in the CustomerDetails table 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CustomerDetails>> GetCustomerDetailsListAsync()
        {
            var customerDetailsList = await _context.CustomerDetails.ToListAsync();
            return customerDetailsList;
        }
        /// <summary>
        /// Searches for the given CustomerDetails by customerId. If found, it will update that customerdetails with the new information
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        public async Task UpdateCustomerDetails(CustomerDTO customerDTO)
        {
            var CustomerToUpdate = await GetCustomerDetailsAsync(customerDTO.CustomerID);
            if (CustomerToUpdate == null)
            { }
            else
            {
                CustomerToUpdate.FirstName = customerDTO.FirstName;
                CustomerToUpdate.LastName = customerDTO.LastName;
                CustomerToUpdate.Email = customerDTO.Email;
                _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Searches for the given CustomerDetails by custoemrId. If found, it will that customerdetails with the new information
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task DeleteCustomerDetails(string customerId)
        {
            var CustomerToDelete = await GetCustomerDetailsAsync(customerId);
            if (CustomerToDelete == null)
            { }
            else
            {
                _context.CustomerDetails.Remove(CustomerToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
