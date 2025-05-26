using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UnitTest
{
    [TestClass]
    public sealed class CustomerServiceTest
    {
        public ICustomerService service { get; set; } = new CustomerService();
        public IAddressService addressService { get; set; } =new AddressService();
        [TestMethod]
        public void GetCustomerDetailsToList()
        {
            var customerList = service.GetCustomerDetailsListAsync().Result;
            Assert.IsNotNull(customerList);
        }
        [TestMethod]
        public void GetSingleCustomerDetails()
        {
            List<CustomerDetails> customerList = (List<CustomerDetails>)service.GetCustomerDetailsListAsync().Result;
            string customerId = customerList[0].CustomerID;
            var customerDetails = service.GetCustomerDetailsAsync(customerId).Result;
            Assert.IsNotNull(customerDetails);
        }
        [TestMethod]
        public void GetCustomersToList()
        {
            var customerList = service.GetCustomersAsync().Result;
            Assert.IsNotNull(customerList);
        }
        [TestMethod]
        public void GetSingleCustomer()
        {
            var customer = service.GetCustomerAsync("k1").Result;
            Assert.IsNotNull(customer);
        }
        [TestMethod]
        public async Task CreateCustomer()
        {
            var totalCustomers = service.GetCustomersAsync();
            var totalCustomerAmount = totalCustomers.Result.Count();
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.FirstName = "TestFirst01";
            customerDTO.LastName = "TestLast01";
            customerDTO.Email = "email";
            customerDTO.AddressDTO.PostalCodeId = "0001";
            customerDTO.AddressDTO.BusNumber = 3;
            customerDTO.AddressDTO.StreetHouse = "testStreet99";
            await service.CreateCustomer(customerDTO);
            var newTotalCustomers = service.GetCustomersAsync();
            var newCustomerAmount = newTotalCustomers.Result.Count();
            totalCustomerAmount++;
            Assert.AreEqual(totalCustomerAmount, newCustomerAmount);
        }
        [TestMethod]
        public void DeleteCustomerDetailsAndAddress()
        {
            List<CustomerDetails> customerList = (List<CustomerDetails>)service.GetCustomerDetailsListAsync().Result;
            CustomerDetails latestCustomer = customerList.Find(c => c.Email == "email");
            string latestCustomerId =latestCustomer.CustomerID;
            service.DeleteCustomerDetails(latestCustomerId).Wait();
            List<CustomerDetails> newCustomerAmount = (List<CustomerDetails>)service.GetCustomerDetailsListAsync().Result;
            Assert.AreNotEqual(newCustomerAmount.Count(), customerList.Count());
        }
    }
}
