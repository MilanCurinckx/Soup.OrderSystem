using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects.User;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private IUserServiceAsync _userServiceAsync;
        public UserController(IUserServiceAsync userServiceAsync)
        {
            _userServiceAsync = userServiceAsync;
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> GetUsers()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            List<UserDetails> userDetailsList = await _userServiceAsync.GetUserList();
            userDTOs = userDetailsList.Select(u => new UserDTO
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                PassWordHash = u.PassWordHash,
            }
            ).ToList();
            return View(userDTOs);
        }
        public async Task<IActionResult> Update(int id)
        {
            UserDTO userDTO = new();
            UserDetails user = await _userServiceAsync.GetUserDetails(id);
            userDTO.UserId = user.UserId;
            userDTO.FirstName = user.FirstName;
            userDTO.LastName = user.LastName;
            userDTO.PassWordHash = user.PassWordHash;
            return View(userDTO);
        }
        public async Task<ActionResult> Delete(int id)
        {
            await _userServiceAsync.DeleteUser(id);
            return RedirectToAction("Users");
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                await _userServiceAsync.CreateUser(userDTO);
            }
            return RedirectToAction("GetUsers");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                await _userServiceAsync.UpdateUser(userDTO);
                return RedirectToAction("getusers");
            }
            else
            {
                return View(userDTO);
            }
        }

    }
}
