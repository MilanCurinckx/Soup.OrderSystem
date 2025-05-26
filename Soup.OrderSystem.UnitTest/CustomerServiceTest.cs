using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

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
            var customerDetails = service.GetCustomerDetailsAsync("t1").Result;
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
            var customer = service.GetCustomerAsync("t1").Result;
            Assert.IsNotNull(customer);
        }
        [TestMethod]
        public void CreateCustomer()
        {
            var totalCustomers = service.GetCustomersAsync();
            var totalCustomerAmount = totalCustomers.Result.Count();
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.FirstName = "TestFirst01";
            customerDTO.CustomerID = "t3";
            customerDTO.LastName = "TestLast01";
            customerDTO.Email = "email";
            customerDTO.AddressDTO.PostalCodeId = "0001";
            //customerDTO.AddressDTO.BusNumber = 3;
            //customerDTO.AddressDTO.StreetHouse = "testStreet99";
            service.CreateCustomer(customerDTO).Wait();
            var newTotalCustomers = service.GetCustomersAsync();
            var newCustomerAmount = newTotalCustomers.Result.Count();
            totalCustomerAmount++;
            Assert.AreEqual(totalCustomerAmount, newCustomerAmount);
        }
        [TestMethod]
        public void DeleteCustomerDetailsAndAddress()
        {
            var customerList = service.GetCustomersAsync().Result;
            var latestCustomer = customerList.Last().CustomerId;
            service.DeleteCustomerDetails(latestCustomer).Wait();
            var customerAmount = customerList.Count();
            var newCustomerAmount = service.GetCustomersAsync().Result.Count();
            Assert.AreNotEqual(newCustomerAmount, customerAmount);
        }
    }
}
