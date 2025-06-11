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

                var claims = new List<Claim>
               {
                   new Claim(ClaimTypes.NameIdentifier, userDetails.UserId.ToString()),
                   new Claim(ClaimTypes.Name,model.FirstName+model.LastName),
                   new Claim(ClaimTypes.Role,"Admin","true")
                   
               };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Admin","Home");
            }

        }
        public IActionResult CustomerLogin()
        {
            return View();
        }
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
                var claims = new List<Claim>
               {
                   new Claim(ClaimTypes.NameIdentifier, customer.CustomerID),
                   new Claim(ClaimTypes.Name,model.FirstName+model.LastName),
                   new Claim(ClaimTypes.Role,"Customer","true")
               };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
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
        //public IActionResult LoginTest()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Overview", "Product");

        }
    }
}
