﻿using Microsoft.AspNetCore.Authorization;
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
        //unused method, do ignore
        public IActionResult Customers()
        {
            return View();
        }
        /// <summary>
        /// redirect to view
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateCustomer()
        {
            return View();
        }
        /// <summary>
        /// Creates a new customer. If parameters are invalid you get sent back to the form.
        /// If successful you get sent back to the main page
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
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
                return RedirectToAction("Overview", "Products");
            }
            else
            {
                return View(customerModel);
            }
        }
        /// <summary>
        /// Shows a list of every customer
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
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
        /// <summary>
        /// Grabs the details of a customer to put into a form for UpdateCustomer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
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
        /// <summary>
        /// Updates a customer and redirects to 
        /// </summary>
        /// <param name="customerModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        /// <summary>
        /// Deletes a customer 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _customerServiceAsync.DeleteCustomerDetails(id);
            return RedirectToAction("GetCustomers");
        }
    }
}
