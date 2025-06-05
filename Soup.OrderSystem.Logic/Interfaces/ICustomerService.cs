using Soup.OrderSystem.Objects.Customer;


namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface ICustomerService
    {
        void CreateCustomer(CustomerDetails customer, Ordersystem.Objects.Customer.Address address);
        Task CreateCustomerAsync(CustomerDetails customer, Ordersystem.Objects.Customer.Address address);
        int CreateCustomerID();
        void DeleteCustomerDetails(string customerId);
        CustomerDetails GetCustomerDetails(string customerId);
        List<CustomerDetails> GetCustomerDetailsList();
        List<Customer> GetCustomers();
        void UpdateCustomerDetails(CustomerDetails customerDetails);
    }
}