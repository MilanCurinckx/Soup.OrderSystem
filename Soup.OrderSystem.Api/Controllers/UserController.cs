using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserServiceAsync _userService;
        public UserController(IUserServiceAsync service)
        {
            _userService = service;
        }
        [HttpPost]
        public async Task CreateUser(UserDTO userDTO)
        {
           await _userService.CreateUser(userDTO);
        }
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var User = await _userService.GetUser(id);
            return Ok(User);
        }
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails(int id)
        {
            var User = await _userService.GetUserDetails(id);
            return Ok(User);
        }
        [HttpGet("GetUserList")]
        public async Task<IActionResult> GetUserList()
        {
            var User = await _userService.GetUserList();
            return Ok(User);
        }
        [HttpPatch]
        public async Task UpdateUser(UserDTO userDTO)
        {
            await _userService.UpdateUser(userDTO);
        }
        [HttpDelete]
        public async Task DeleteUser(int id)
        {
             await _userService.DeleteUser(id);
        }
    }
}
