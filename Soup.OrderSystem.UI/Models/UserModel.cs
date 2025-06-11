using Soup.OrderSystem.Logic.DTO;
using System.Security.Claims;

namespace Soup.OrderSystem.UI.Models
{
    public class UserModel: UserDTO
    {
        public List<Claim> Claims {  get; set; }
    }
}
