using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Objects.User;
using Soup.OrderSystem.UI.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Controllers
{
    public class LoginController : Controller
    {
        private IUserServiceAsync _userService;
        private ICustomerServiceAsync _customerService;
        private IOrderServiceAsync _orderService;
        public LoginController(IUserServiceAsync userService, ICustomerServiceAsync customerService, IOrderServiceAsync orderService)
        {
            _customerService = customerService;
            _userService = userService;
            _orderService = orderService;
        }
        public IActionResult UserLogin()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Admin", "Home");
            }
            else
            {
                return View();
            }
                
        }
        /// <summary>
        /// Checks if the credentials are valid, if true a cookie with the login is created and the user gets redirected to the admin page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UserLogin(UserModel model)
        {
            List<UserDetails> userDetailsList = await _userService.GetUserList();
            UserDetails? userDetails = userDetailsList.FirstOrDefault(u => u.FirstName == model.FirstName && u.LastName == model.LastName && u.PassWordHash == model.PassWordHash);
            if (userDetails == null)
            {
                ViewData["Error"] = "Invalid credentials";
                return View(model);
            }
            else
            {
                //Saves the user id, the user's first and last name and their role into a claim
                var claims = new List<Claim>
               {
                   new Claim(ClaimTypes.NameIdentifier, userDetails.UserId.ToString()),
                   new Claim(ClaimTypes.Name,model.FirstName+model.LastName),
                   new Claim(ClaimTypes.Role,"Admin","true")
                   
               };
                //create the claim
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                // add the identity to your principal
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Admin","Home");
            }

        }
        public IActionResult CustomerLogin()
        {
            return View();
        }
        /// <summary>
        /// Checks if the credentials are valid, if true a cookie with the login is created and the customer gets redirected to the main page
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CustomerLogin(CustomerModel model)
        {
            List<CustomerDetails> customerDetails = await _customerService.GetCustomerDetailsList();
            CustomerDetails? customer = customerDetails.FirstOrDefault(c => c.FirstName == model.FirstName && c.LastName == model.LastName && c.Email == model.Email);
            int orderId = 0;
            if (customer == null)
            {
                ViewData["Error"] = "Invalid credentials";
                return View(model);
            }
            else
            {
                //Saves the customer id, the user's first and last name and their role into a claim
                var claims = new List<Claim>
               {
                   new Claim(ClaimTypes.NameIdentifier, customer.CustomerID),
                   new Claim(ClaimTypes.Name,model.FirstName+model.LastName),
                   new Claim(ClaimTypes.Role,"Customer","true")
               };
                //create the claim
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                // add the identity to your principal
                var principal = new ClaimsPrincipal(claimsIdentity);
                //login
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                //if an order hasn't been created yet (which it shouldn't at this point but you never know), create a new order with the Customer id and save it in a session cookie 
                if (HttpContext.Session.GetInt32("OrderId") == null)
                {
                    orderId = await _orderService.CreateOrder(customer.CustomerID);
                    HttpContext.Session.SetInt32("OrderId", orderId);
                }
                return RedirectToAction("Overview","Product");
            }
        }
        public IActionResult Forbidden()
        {
            return View();
        }
        //unused method from when I was testing Login
        //public IActionResult LoginTest()
        //{
        //    return View();
        //}
        /// <summary>
        /// Logs a user or customer out and redirects to the main page
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Overview", "Product");

        }
    }
}
