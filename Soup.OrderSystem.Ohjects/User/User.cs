using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Objects.User
{
    public class User
    {
        public User()
        {
            UserDetails userDetails = new();
            UserId userId = new ();
            User_Id = userId;
            Details = userDetails;
 
        }
        public UserId User_Id { get; set; }
        public UserDetails Details { get; set; }
    }
}
