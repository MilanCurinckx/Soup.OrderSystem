using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
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
        private ICustomerServiceAsync _customerService { get; set; } = new CustomerServiceAsync();
        [Fact]
        public async Task Test1()
        {
            CustomerDTO newCustomer = new();
            AddressDTO addressDTO = new AddressDTO();
            List<Customer> CustomerList =await _customerService.GetCustomers();
            int totalCustomers = CustomerList.Count;
            newCustomer.FirstName = "FirstNameTest";
            newCustomer.LastName = "LastNameTest";
            newCustomer.Email = "emailTest";
            addressDTO.StreetHouse = "address";
            addressDTO.BusNumber = 69;
            addressDTO.PostalCodeId = "0001";
            await _customerService.CreateCustomer(newCustomer,addressDTO);
            List<Customer> newCustomerList = await _customerService.GetCustomers();
            int newTotalCustomers = newCustomerList.Count;
            Assert.NotEqual(totalCustomers, newTotalCustomers);
        }
        [Fact]
        public async Task Test2()
        {
            CustomerDTO customer = new();
            List<CustomerDetails> customerDetails = await _customerService.GetCustomerDetailsList();
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
                customer.CustomerID = customerToUpdate.CustomerID;
                customer.FirstName = customerToUpdate.FirstName;
                customer.LastName = customerToUpdate.LastName;
                customer.Email = customerToUpdate.Email;
                customer.AddressId = customer.AddressId;
                await _customerService.UpdateCustomerDetails(customer);
                CustomerDetails updatedCustomer =await _customerService.GetCustomerDetails(customer.CustomerID);
                Assert.True(updatedCustomer.Email == teststring);
            }
        }
        [Fact]
        public async Task Test3()
        {
            
            List<CustomerDetails> customerDetails = await _customerService.GetCustomerDetailsList();
            int currentAmount = customerDetails.Count;
            CustomerDetails customerToUpdate = customerDetails.Where(c => c.FirstName == "FirstNameTest").FirstOrDefault();
            if (customerToUpdate != null)
            {
               await _customerService.DeleteCustomerDetails(customerToUpdate.CustomerID);
            }
            List<CustomerDetails> newCustomerDetails = await _customerService.GetCustomerDetailsList();
            int newAmount = newCustomerDetails.Count;
            Assert.NotEqual(currentAmount, newAmount);
        }
    }
}
