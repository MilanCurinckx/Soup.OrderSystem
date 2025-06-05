using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Objects.User;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Controllers
{
    public class UserController : Controller
    {
        private IUserServiceAsync _userServiceAsync;
        public UserController(IUserServiceAsync userServiceAsync)
        {
            _userServiceAsync = userServiceAsync;
        }
        [HttpPost]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> GetUsers()
        {
          List<UserDetails> userDetailsList = await _userServiceAsync.GetUserList();
          ViewData["UserDetailsList"] = userDetailsList;
            return View(userDetailsList);
        }
        public async Task<IActionResult> GetUser()
        {
            return View();
        }
        public IActionResult BackEndCreate(UserDTO userDTO)
        {
            _userServiceAsync.CreateUser(userDTO);
            return Create();
        }

    }
}
