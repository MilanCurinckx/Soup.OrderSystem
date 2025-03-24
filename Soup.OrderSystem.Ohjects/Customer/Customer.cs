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
           
            CustomerDetails = new CustomerDetails();
            Address = new Address();
            CustomerId = new CustomerId();
         
        }
        public CustomerId CustomerId { get; set; }
        public CustomerDetails CustomerDetails { get; set; }
        public Address Address { get; set; }
    }
}
