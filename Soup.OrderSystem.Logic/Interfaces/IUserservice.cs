using Soup.Ordersystem.Objects.User;
using Soup.OrderSystem.Logic.DTO;

namespace Soup.OrderSystem.Logic
{
    public interface IUserservice
    {
        Task CreateUserAsync(UserDTO userDTO);
        Task DeleteUserAsync(int id);
        Task<Users> GetUserAsync(int id);
        Task<UserDetails> GetUserDetailsAsync(int id);
        Task<IEnumerable<UserDetails>> GetUserListAsync();
        Task UpdateUserAsync(UserDTO userDTO);
    }
}