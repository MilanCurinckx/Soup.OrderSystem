using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderServiceAsync _orderService;
        public OrderController(IOrderServiceAsync service)
        {
            _orderService = service;
        }
        public IActionResult Add(int id)
        {
            return View();
        }
    }
}
