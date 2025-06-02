using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerServiceAsync _customerService;
        public CustomerController(ICustomerServiceAsync customer)
        {
            _customerService = customer;
        }
        [HttpPost]
        public async Task Create(CustomerDTO customerDTO)
        {
           await _customerService.CreateCustomer(customerDTO);
        }
        [HttpGet]
        public async Task<IActionResult>GetCustomersList()
        {
          var customerList= await _customerService.GetCustomers();
            return Ok(customerList);
        }
        [HttpGet("GetCustomerDetailsList")]
        public async Task<IActionResult> GetCustomerDetailsList()
        {
            var customerList = await _customerService.GetCustomerDetailsList();
            return Ok(customerList);
        }
        [HttpGet("GetCustomerDetail")]
        public async Task<IActionResult> GetCustomerDetail(string customerId)
        {
            var customerDetails = _customerService.GetCustomerDetails(customerId);
            return Ok(customerDetails);
        }
        [HttpPatch]
        public async Task Update(CustomerDTO customerDTO)
        {
          await _customerService.UpdateCustomerDetails(customerDTO);
        }
        [HttpDelete]
        public async Task Delete(CustomerDTO customerDTO)
        {
            await _customerService.DeleteCustomerDetails(customerDTO.CustomerID);
        }
    }
}
