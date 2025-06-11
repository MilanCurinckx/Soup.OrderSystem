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
        //unused method
        //public IActionResult Users()
        //{
        //    return View();
        //}
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// get a list of all users & converts them to a list of UserDTO's to return
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// loads the userdetails required to update a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// deletes a user with the given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            await _userServiceAsync.DeleteUser(id);
            return RedirectToAction("Users");
        }
        /// <summary>
        /// Creates a User and redirects to the List of users
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                await _userServiceAsync.CreateUser(userDTO);
            }
            return RedirectToAction("GetUsers");
        }
        /// <summary>
        /// updates a user and redirects to the list of getUsers.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
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
