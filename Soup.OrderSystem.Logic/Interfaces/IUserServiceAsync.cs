using Soup.Ordersystem.Objects.User;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public interface IUserServiceAsync
    {
        Task CreateUser(UserDTO userDTO);
        Task DeleteUser(int id);
        Task<Users> GetUser(int id);
        Task<UserDetails> GetUserDetails(int id);
        Task<List<UserDetails>> GetUserList();
        Task UpdateUser(UserDTO userDTO);
    }
}