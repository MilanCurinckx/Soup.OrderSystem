using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Soup.OrderSystem.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
       
        public OrderController()
        {
            
        }
        public IActionResult CreateOrder()
        {
            return View();
        }
        public IActionResult getOrders()
        {
            return View();
        }
        public IActionResult DeleteOrder()
        {
            return View();
        }
        public IActionResult ChangeOrderStatus()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateOrder()
        {
            return View();
        }
    }
}
