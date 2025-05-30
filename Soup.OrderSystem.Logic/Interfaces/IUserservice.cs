using Soup.Ordersystem.Objects.User;
namespace Soup.OrderSystem.Logic
{
    public interface IUserservice
    {
        void CreateUser(UserDetails users);
        void DeleteUser(int id);
        Users GetUser(int id);
        UserDetails GetUserDetails(int id);
        List<UserDetails> GetUserList();
        void UpdateUser(UserDetails users);
    }
}