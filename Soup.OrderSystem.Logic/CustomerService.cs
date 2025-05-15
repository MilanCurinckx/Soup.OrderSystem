using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public class CustomerService 
    {
        private OrderContext _context = new();
        private IAddressService _addressService;

        public CustomerService(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<int> CreateCustomerID()
        {
            var customerList = await GetCustomersAsync();
            var latestCustomer = customerList.LastOrDefault().CustomerId;
            latestCustomer = latestCustomer.Substring(1);
            int newCustomerId = int.Parse(latestCustomer);
            newCustomerId++;
            return newCustomerId;          
        }
        public async Task CreateCustomer(CustomerDTO customerDTO)
        {
            int customerId = await CreateCustomerID();
            Customer customer = new(customerId);
            await _context.SaveChangesAsync();
            CustomerDetails customerDetails = new();
            customerDetails.CustomerID = customer.CustomerId;
            customerDetails.FirstName = customerDTO.FirstName;
            customerDetails.LastName = customerDTO.LastName;
            customerDetails.Email = customerDTO.Email;
            Address newAddress =await _addressService.CreateAddress(customerDTO.AddressDTO);
            customer.AddressId= newAddress.AddressID; 
            await _context.SaveChangesAsync();

        }
        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            var customer = await _context.Customer.Where(c => c.CustomerId == customerId).FirstOrDefaultAsync();
            return customer;
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customer = await _context.Customer.ToListAsync();
            return customer;
        }

        public async Task<CustomerDetails> GetCustomerDetailsAsync(string customerId)
        {
            var customerDetails = await _context.CustomerDetails.Where(c => c.CustomerID == customerId).FirstOrDefaultAsync();
            return customerDetails;
        }
        public async  Task<IEnumerable<CustomerDetails>>GetCustomerDetailsListAsync()
        {
           var customerDetailsList = await _context.CustomerDetails.ToListAsync(); 
            return customerDetailsList;
        }

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
                 
            }
        }
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
