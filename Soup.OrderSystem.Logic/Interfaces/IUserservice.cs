using Soup.OrderSystem.Objects.User;
namespace Soup.OrderSystem.Logic.Interfaces
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