using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.UI.Models;
using System.Diagnostics;

namespace Soup.OrderSystem.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Admin", "Home");
            }
            else
            {
                return RedirectToAction("Overview", "Product");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// shows the admin page
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles ="Admin")]
        public IActionResult Admin()
        {
            return View();
        }
        
    }
}
