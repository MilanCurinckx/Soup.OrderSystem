using Soup.Ordersystem.Objects.User;
using Soup.OrderSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UnitTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void Test1()
        {
            IUserservice userService = new Userservice();
            UserDetails userDetails = new UserDetails();
            userDetails.FirstName = "UserFirst";
            userDetails.LastName = "UserLast";
            userDetails.PassWordHash = "teststring";
            userService.CreateUser(userDetails);
            List<UserDetails> userList = userService.GetUserList();
            UserDetails latestUser = userList.FindLast(x => x.FirstName == userDetails.FirstName);
            Assert.IsNotNull(latestUser);
        }
        //[TestMethod]
        //public void Test3()
        //{
        //    IUserservice userservice1 = new Userservice();
        //    List<UserDetails> users = userservice1.GetUserList();
        //    Assert.IsNotNull(users);
        //}
        [TestMethod]
        public void Test2()
        {
            IUserservice userService = new Userservice();
            UserDetails user = userService.GetUserList().Last();
            if (user.PassWordHash == "teststring")
            {
                user.PassWordHash = "updated";
            }
            else
            {
                user.PassWordHash = "teststring";
            }
            userService.UpdateUser(user);
            UserDetails updatedDetails = userService.GetUserList().Last();
            Assert.IsTrue(user != updatedDetails);
        }
        [TestMethod]
        public void Test4()
        {
            IUserservice userService = new Userservice();
            UserDetails user = userService.GetUserList().Last();
            userService.DeleteUser(user.UserId);
            UserDetails? deletedUser = userService.GetUserDetails(user.UserId);
            Assert.IsNull(deletedUser);
        }
    }
}
