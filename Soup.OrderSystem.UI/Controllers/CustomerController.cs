using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.UI.Models;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Controllers
{

    public class CustomerController : Controller
    {
        private ICustomerServiceAsync _customerServiceAsync;
        private IAddressServiceAsync _addressServiceAsync;
        public CustomerController(ICustomerServiceAsync service, IAddressServiceAsync addressService)
        {
            _customerServiceAsync = service;
            _addressServiceAsync = addressService;
        }
        public IActionResult Customers()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult CreateCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerModel customerModel)
        {
            if (ModelState.IsValid)
            {
                CustomerDTO customerDTO = new CustomerDTO();
                customerDTO.FirstName = customerModel.FirstName;
                customerDTO.LastName = customerModel.LastName;
                customerDTO.Email = customerModel.Email;
                AddressDTO addressDTO = new AddressDTO();
                addressDTO.BusNumber = customerModel.BusNumber;
                addressDTO.PostalCodeId = customerModel.PostalCodeId;
                addressDTO.StreetHouse = customerModel.StreetHouse;
                await _customerServiceAsync.CreateCustomer(customerDTO, addressDTO);
                return RedirectToAction("GetCustomers");
            }
            else
            {
                return View(customerModel);
            }
        }
        public async Task<IActionResult> GetCustomers()
        {
            List<CustomerModel> customerModelsList = new();
            List<CustomerDetails> customerDetailsList = await _customerServiceAsync.GetCustomerDetailsList();
            foreach (CustomerDetails customerDetails in customerDetailsList)
            {
                Address address = await _addressServiceAsync.GetAddressById(customerDetails.AddressId);
                CustomerModel customerModel = new CustomerModel();
                customerModel.CustomerID = customerDetails.CustomerID;
                customerModel.FirstName = customerDetails.FirstName;
                customerModel.LastName = customerDetails.LastName;
                customerModel.Email = customerDetails.Email;
                customerModel.AddressId = customerDetails.AddressId;
                customerModel.StreetHouse = address.StreetHouse;
                customerModel.BusNumber = address.BusNumber;
                customerModel.PostalCodeId = address.PostalCodeId;
                customerModelsList.Add(customerModel);
            }
            return View(customerModelsList);
        }
        public async Task<IActionResult> Update(string id)
        {
            CustomerDetails customerDetails = new CustomerDetails();
            Address address = new Address();
            CustomerModel customerModel = new CustomerModel();
            customerDetails = await _customerServiceAsync.GetCustomerDetails(id);
            address = await _addressServiceAsync.GetAddressById(customerDetails.AddressId);
            customerModel.CustomerID = customerDetails.CustomerID;
            customerModel.FirstName = customerDetails.FirstName;
            customerModel.LastName = customerDetails.LastName;
            customerModel.Email = customerDetails.Email;
            customerModel.AddressId = customerDetails.AddressId;
            customerModel.StreetHouse = address.StreetHouse;
            customerModel.BusNumber = address.BusNumber;
            customerModel.PostalCodeId = address.PostalCodeId;
            return View(customerModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(CustomerModel customerModel)
        {
            AddressDTO addressDTO = new AddressDTO();
            addressDTO.StreetHouse = customerModel.StreetHouse;
            addressDTO.AddressID = (int)customerModel.AddressId;
            addressDTO.BusNumber = customerModel.BusNumber;
            addressDTO.PostalCodeId = customerModel.PostalCodeId;
            CustomerDTO customerDTO = new CustomerDTO();
            customerDTO.FirstName = customerModel.FirstName;
            customerDTO.LastName = customerModel.LastName;
            customerDTO.Email = customerModel.Email;
            customerDTO.CustomerID = customerModel.CustomerID;
            customerDTO.AddressId = (int)customerModel.AddressId;
            await _customerServiceAsync.UpdateCustomerDetails(customerDTO);
            await _addressServiceAsync.UpdateAddress(addressDTO);
            return RedirectToAction("GetCustomers");
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _customerServiceAsync.DeleteCustomerDetails(id);
            return RedirectToAction("GetCustomers");
        }
    }
}
