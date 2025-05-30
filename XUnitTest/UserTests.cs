using Soup.OrderSystem.Logic;
using Soup.Ordersystem.Objects.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.XunitTests
{
    public class UserTests
    {
        private IUserservice _userService { get; set; } = new Userservice();
        [Fact]
        public void Test1()
        {
            UserDetails userDetails = new UserDetails();
            userDetails.FirstName = "UserFirst";
            userDetails.LastName = "UserLast";
            userDetails.PassWordHash = "teststring";
            _userService.CreateUser(userDetails);
            List<UserDetails> userList = _userService.GetUserList();
            UserDetails latestUser = userList.FindLast(x => x.FirstName == userDetails.FirstName);
            Assert.NotNull(latestUser);
        }
        [Fact]
        public void Test2()
        {
            UserDetails user = _userService.GetUserList().Last();
            if (user.PassWordHash == "teststring")
            {
                user.PassWordHash = "updated";
            }
            else
            {
                user.PassWordHash = "teststring";
            }
            _userService.UpdateUser(user);
            UserDetails updatedDetails = _userService.GetUserList().Last();
            Assert.True(user.PassWordHash == updatedDetails.PassWordHash);
        }
        [Fact]
        public void Test3()
        {
            UserDetails user = _userService.GetUserList().Last();
            _userService.DeleteUser(user.UserId);
            UserDetails? deletedUser = _userService.GetUserDetails(user.UserId);
            Assert.Null(deletedUser);
        }
    }
}
