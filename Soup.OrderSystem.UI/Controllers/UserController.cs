using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.UI.Controllers
{
    public class UserController : Controller
    {
        private IUserServiceAsync _userServiceAsync;
        public UserController(IUserServiceAsync userServiceAsync)
        {
            _userServiceAsync = userServiceAsync;
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult backEndCreate(UserDTO userDTO)
        {
            _userServiceAsync.CreateUser(userDTO);
            return Create();
        }

    }
}
