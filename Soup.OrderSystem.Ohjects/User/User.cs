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
            UserDetails userDetails = new UserDetails {};
            Details = userDetails;
            Details.User_Id = User_Id;
        }
        public int User_Id { get; set; }
        public UserDetails Details { get; set; }
    }
}
