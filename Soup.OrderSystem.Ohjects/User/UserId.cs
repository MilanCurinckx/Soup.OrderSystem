using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.Objects.User
{
    public class UserId
    {
        [Key]
        public int User_Id { get; set; }

    }
}

