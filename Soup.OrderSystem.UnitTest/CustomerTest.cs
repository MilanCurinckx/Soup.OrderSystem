using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UnitTest
{
    [TestClass]
    public class CustomerTest
    {
        //public ICustomerService CustomerService { get; set; } = new CustomerService();
        //public IAddressService AddressService { get; set; }
        [TestMethod]
        public void Test1()
        {
            ICustomerService customerService = new CustomerService();
            List<Customer> CustomerList = customerService.GetCustomers();
            int totalCustomers = CustomerList.Count;
            CustomerDetails newCustomer = new();
            newCustomer.FirstName = "FirstNameTest";
            newCustomer.LastName = "LastNameTest";
            newCustomer.Email = "emailTest";
            Address address = new Address();
            address.StreetHouse = "address";
            address.BusNumber = 69;
            address.PostalCodeId = "0001";
            customerService.CreateCustomer(newCustomer, address);

        }
        [TestMethod]
        public void Test2()
        {
            ICustomerService customersService = new CustomerService();
            List<CustomerDetails> customerDetails = customersService.GetCustomerDetailsList();
            CustomerDetails customerToUpdate = customerDetails.Where(c => c.FirstName == "FirstNameTest").FirstOrDefault();
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
                customersService.UpdateCustomerDetails(customerToUpdate);
                CustomerDetails updatedCustomer = customersService.GetCustomerDetails(customerToUpdate.CustomerID);
                Assert.IsTrue(updatedCustomer.Email == teststring);
            }
        }
        [TestMethod]
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
            Assert.AreNotEqual(currentAmount, newAmount);
        }
    }
}
