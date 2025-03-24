using Soup.OrderSystem.Data;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Objects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    
    public class CustomerService
    {
        private readonly OrderContext _orderContext;
        public CustomerService(OrderContext context)
        {
            this._orderContext = context;
        }

        public void CreateCustomer(CustomerDTO customerDTO)
        {
            Customer customer = new();
            customer.CustomerDetails.FirstName = customerDTO.FirstName;
            customer.CustomerDetails.LastName = customerDTO.LastName;
            customer.CustomerDetails.Email = customerDTO.Email;
            
                        
        }
    }
    
}
