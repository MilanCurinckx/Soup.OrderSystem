using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.XunitTests
{
    public class CustomerServiceTest
    {
        private ICustomerService _customerService { get; set; } = new CustomerService();
        [Fact]
        public void Test1()
        {
            List<Customer> CustomerList = _customerService.GetCustomers();
            int totalCustomers = CustomerList.Count;
            CustomerDetails newCustomer = new();
            newCustomer.FirstName = "FirstNameTest";
            newCustomer.LastName = "LastNameTest";
            newCustomer.Email = "emailTest";
            Address address = new Address();
            address.StreetHouse = "address";
            address.BusNumber = 69;
            address.PostalCodeId = "0001";
            _customerService.CreateCustomer(newCustomer, address);
            List<Customer> newCustomerList = _customerService.GetCustomers();
            int newTotalCustomers = newCustomerList.Count;
            Assert.NotEqual(totalCustomers, newTotalCustomers);
        }
        [Fact]
        public void Test2()
        {

            List<CustomerDetails> customerDetails = _customerService.GetCustomerDetailsList();
            CustomerDetails customerToUpdate = customerDetails.Where(c => c.FirstName == "FirstNameTest").Last();
            if (customerToUpdate != null)
            {
                string updateString1 = "AAAAAAH";
                string updateString2 = "Hi";
                string teststring = "";
                if (customerToUpdate.Email == updateString1)
                {
                    customerToUpdate.Email = updateString2;
                    teststring = updateString2;
                }
                else
                {
                    customerToUpdate.Email = updateString1;
                    teststring = updateString1;
                }
                _customerService.UpdateCustomerDetails(customerToUpdate);
                CustomerDetails updatedCustomer = _customerService.GetCustomerDetails(customerToUpdate.CustomerID);
                Assert.True(updatedCustomer.Email == teststring);
            }
        }
        [Fact]
        public void Test3()
        {
            ICustomerService customerService = new CustomerService();
            List<CustomerDetails> customerDetails = customerService.GetCustomerDetailsList();
            int currentAmount = customerDetails.Count;
            CustomerDetails customerToUpdate = customerDetails.Where(c => c.FirstName == "FirstNameTest").FirstOrDefault();
            if (customerToUpdate != null)
            {
                customerService.DeleteCustomerDetails(customerToUpdate.CustomerID);
            }
            List<CustomerDetails> newCustomerDetails = customerService.GetCustomerDetailsList();
            int newAmount = newCustomerDetails.Count;
            Assert.NotEqual(currentAmount, newAmount);
        }
    }
}
