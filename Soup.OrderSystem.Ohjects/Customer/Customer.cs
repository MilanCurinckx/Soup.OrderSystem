using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Objects.Customer
{
    public class Customer
    {
        public Customer() 
        {
            CustomerId customerId = new CustomerId();
            CustomerDetails customerDetails = new CustomerDetails();
            Address address = new Address();
            CustomerId = customerId;
            CustomerDetails = customerDetails;
            Address = address;
        }
        public CustomerId CustomerId { get; set; }
        public CustomerDetails CustomerDetails { get; set; }
        public Address Address { get; set; }
    }
}
