﻿using Soup.Ordersystem.Objects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic.DTO
{
    public class UserDTO 
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PassWordHash { get; set; }
    }
}
