using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.Objects.User
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }
    }
}
