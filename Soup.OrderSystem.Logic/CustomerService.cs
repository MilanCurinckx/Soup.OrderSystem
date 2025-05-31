using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    public class CustomerService : ICustomerService
    {

        private AddressService _addressService { get; set; } = new AddressService();

        /// <summary>
        /// generates a new customerId by looking at the last one in the DB and increasing that by one. this would've been done by EF except for the fact that it's not complete without a 'k' added in front. 
        /// </summary>
        /// <returns></returns>
        public int CreateCustomerID()
        {
            var customerList = GetCustomers();
            List<int> customerIdList = new List<int>();
            foreach (var customer in customerList)
            {
                string customerId = customer.CustomerId.Substring(1);
                int id = int.Parse(customerId);
                customerIdList.Add(id);
            }
            int latestCustomer = customerIdList.Max();
            int newCustomerId = latestCustomer + 1;
            return newCustomerId;
        }
        /// <summary>
        /// Creates a new customer to be added, calls upon CreateCustomerId to create a new Id which afterwards gets the 'k' char concatenated. Also immediately creates a customerDetails which is linked to CustomerId & calls on addressService to create a new Address for the customer (unless the address already exists => look at addressservice for that)
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        public void CreateCustomer(CustomerDetails customer, Ordersystem.Objects.Customer.Address address)
        {
            try
            {
                Ordersystem.Objects.Customer.Address newAddress = _addressService.CreateAddress(address);
                string customerId = CreateCustomerID().ToString();
                using (OrderContext context = new OrderContext())
                {
                    Customer newCustomer = new();
                    var id = string.Concat('k' + customerId);
                    newCustomer.CustomerId = id;
                    context.Add(newCustomer);
                    context.SaveChanges();
                    CustomerDetails customerDetails = new();
                    customerDetails.CustomerID = newCustomer.CustomerId;
                    customerDetails.FirstName = customer.FirstName;
                    customerDetails.LastName = customer.LastName;
                    customerDetails.Email = customer.Email;
                    customerDetails.AddressId = newAddress.AddressID;
                    context.Add(customerDetails);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong during the creation of a new customer"+ex.Message);
            }
        }
        public async Task CreateCustomerAsync(CustomerDetails customer,Address address)
        {
            try
            {
                Ordersystem.Objects.Customer.Address newAddress = _addressService.CreateAddress(address);
                string customerId = CreateCustomerID().ToString();
                using (OrderContext context = new OrderContext())
                {
                    Customer newCustomer = new();
                    var id = string.Concat('k' + customerId);
                    newCustomer.CustomerId = id;
                    context.Add(newCustomer);
                    CustomerDetails customerDetails = new();
                    customerDetails.CustomerID = newCustomer.CustomerId;
                    customerDetails.FirstName = customer.FirstName;
                    customerDetails.LastName = customer.LastName;
                    customerDetails.Email = customer.Email;
                    customerDetails.AddressId = newAddress.AddressID;
                    context.Add(customerDetails);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong during the creation of a new customer" + ex.Message);
            }
        }
        /// <summary>
        /// returns a list of every customer in the Customer table. This does NOT returns their details, only Id and and addressId for each. Use GetCustomerDetailsList for more details on each customer
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            try
            {
                using (OrderContext context = new OrderContext())
                {
                    var customer = context.Customer.ToList();
                    return customer;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the customers from the database");
            }
;
        }
        /// <summary>
        /// returns the customerdetails of a specific customer by their id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public CustomerDetails GetCustomerDetails(string customerId)
        {
            try
            {
                using (OrderContext context = new OrderContext())
                {
                    var customerDetails = context.CustomerDetails.Where(c => c.CustomerID == customerId).FirstOrDefault();
                    return customerDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the Customerdetails from the database");
            }
        }
        /// <summary>
        /// return a list of overy customerdetails in the CustomerDetails table 
        /// </summary>
        /// <returns></returns>
        public List<CustomerDetails> GetCustomerDetailsList()
        {
            try
            {
                using (OrderContext context = new OrderContext())
                {
                    var customerDetailsList = context.CustomerDetails.ToList();
                    return customerDetailsList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the Customerdetails from the database");
            }

        }
        /// <summary>
        /// Searches for the given CustomerDetails by customerId. If found, it will update that customerdetails with the new information
        /// </summary>
        /// <param name="customerDTO"></param>
        /// <returns></returns>
        public void UpdateCustomerDetails(CustomerDetails customerdetails)
        {
            try
            {
                var CustomerToUpdate = GetCustomerDetails(customerdetails.CustomerID);
                using (OrderContext context = new OrderContext())
                {
                    if (CustomerToUpdate == null)
                    { }
                    else
                    {
                        CustomerToUpdate.FirstName = customerdetails.FirstName;
                        CustomerToUpdate.LastName = customerdetails.LastName;
                        CustomerToUpdate.Email = customerdetails.Email;
                        context.Update(CustomerToUpdate);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the Customerdetails from the database: " + ex.Message);
            }

        }
        /// <summary>
        /// Searches for the given CustomerDetails by custoemrId. If found, it will that customerdetails with the new information
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public void DeleteCustomerDetails(string customerId)
        {
            try
            {
                var CustomerToDelete = GetCustomerDetails(customerId);
                using (OrderContext context = new OrderContext())
                {
                    if (CustomerToDelete == null)
                    { }
                    else
                    {
                        context.CustomerDetails.Remove(CustomerToDelete);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while deleting the customerdetails");
            }


        }
    }
}
