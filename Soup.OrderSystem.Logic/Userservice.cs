using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects.User;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.Interfaces;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    public class Userservice : IUserservice
    {
        private OrderContext _context = new();
        /// <summary>
        /// Saves a user to the database and creates a UserDetails based on the Id from the new user
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns> </returns>
        public void CreateUser(UserDetails userDetails)
        {
            //first creating a new user
            Users newUser = new();
            _context.Users.Add(newUser);
            _context.SaveChanges();
            //The users class only has Id, and I need to have the same one for Userdetails to link them
            var latestUser = GetUser(newUser.UserID);
            UserDetails newUserDetails = new();
            newUserDetails.UserId = latestUser.UserID;
            newUserDetails.FirstName =userDetails.FirstName;
            newUserDetails.LastName = userDetails.LastName;
            newUserDetails.PassWordHash = userDetails.PassWordHash;
            _context.UserDetails.Add(newUserDetails);
            _context.SaveChanges();
        }
        /// <summary>
        /// Returns the user of the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Users GetUser(int id)
        {
            var user = _context.Users.Where(u => u.UserID == id).FirstOrDefault();
            return user;
        }
        /// <summary>
        /// returns the userdetails of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserDetails GetUserDetails(int id)
        {
            var userDetails = _context.UserDetails.Where(u => u.UserId == id).FirstOrDefault();
            return userDetails;
        }
        /// <summary>
        /// Returns the userdetails of every user in the database as an IEnumerable
        /// </summary>
        /// <returns></returns>
        public List<UserDetails> GetUserList()
        {
            var UserList = _context.UserDetails.ToList();
            return UserList;
        }
        /// <summary>
        /// Updates the userdetails of the passed along userDTO, able to update the first name, last name & Password
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        public void UpdateUser(UserDetails userDetails)
        {
            UserDetails userToUpdate = GetUserDetails(userDetails.UserId);
            userToUpdate.FirstName = userDetails.FirstName;
            userToUpdate.LastName = userDetails.LastName;
            userToUpdate.PassWordHash = userDetails.PassWordHash;
            _context.Update(userToUpdate);
            _context.SaveChanges();

        }
        /// <summary>
        /// Deletes the userdetails of the passed along id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void DeleteUser(int id)
        {
            UserDetails? userToDelete = _context.UserDetails.Where(u => u.UserId == id).FirstOrDefault();
            if (userToDelete == null)
            {
                throw new Exception("User couldn't be found, are you sure you have the right id?");
            }
            else
            {
                _context.UserDetails.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
    }
}
