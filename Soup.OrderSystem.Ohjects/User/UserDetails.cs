using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.User
{
    [Keyless]
    public class UserDetails
    {
        [ForeignKey("User_Id")]
        public int User_Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Password { get; set; }
    }
}

