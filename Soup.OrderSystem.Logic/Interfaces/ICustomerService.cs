using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface ICustomerService
    {
        Task CreateCustomer(CustomerDTO customerDTO);
        Task<int> CreateCustomerID();
        Task DeleteCustomerDetails(string customerId);
        Task<Customer> GetCustomerAsync(string customerId);
        Task<CustomerDetails> GetCustomerDetailsAsync(string customerId);
        Task<IEnumerable<CustomerDetails>> GetCustomerDetailsListAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task UpdateCustomerDetails(CustomerDTO customerDTO);
    }
}