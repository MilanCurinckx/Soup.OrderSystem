using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;

namespace Soup.OrderSystem.Logic
{
    public class CustomerService 
    {
        private OrderContext _context = new();

        static async Task<string> CreateCustomerID()
        {
            CustomerService customerService = new();
            
            var customerList = await customerService.GetCustomersAsync() ;
            var latestCustomer = customerList.LastOrDefault();
            string latestCustomerId = latestCustomer.CustomerID;
            string customerIdString = latestCustomerId.Substring(1);
            int customerIdInt = int.Parse(customerIdString);
            int newCustomerIdInt = customerIdInt + 1;
            string newCustomerIdString = customerIdString.ToString();
            string customerSignifier = "k";
            string customerConcat = customerSignifier.Concat(customerIdString).ToString();
            return customerConcat;

        }
        public async Task CreateCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new();
            string customerId = await CreateCustomerID();
            customer.CustomerID = customerId;
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            CustomerDetails customerDetails = new();
            customerDetails.CustomerID = customerId;
            customerDetails.FirstName = customerDTO.FirstName;
            customerDetails.LastName = customerDTO.LastName;
            customerDetails.Email = customerDTO.Email;
            AddressService addressService = new();
            await addressService.CreateAddress(customerDTO.AddressDTO);


        }
        public async Task<Customer> GetCustomerAsync(string customerId)
        {
            var customer = await _context.Customer.Where(c => c.CustomerID == customerId).FirstOrDefaultAsync();
            return customer;
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customer = await _context.Customer.ToListAsync();
            return customer;
        }
    }
}
