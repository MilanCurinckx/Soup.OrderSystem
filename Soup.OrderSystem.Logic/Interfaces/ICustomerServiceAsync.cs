using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface ICustomerServiceAsync
    {
        Task CreateCustomer(CustomerDTO customer, AddressDTO addressDTO);
        Task<int> CreateCustomerID();
        Task DeleteCustomerDetails(string customerId);
        Task<CustomerDetails?> GetCustomerDetails(string customerId);
        Task<List<CustomerDetails>> GetCustomerDetailsList();
        Task<List<Customer>> GetCustomers();
        Task UpdateCustomerDetails(CustomerDTO customerdetails);
    }
}