using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.User;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    public class Userservice
    {
        private OrderContext _context = new();
        /// <summary>
        /// Saves a user to the database and creates a UserDetails based on the Id from the new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns> </returns>
        public async Task CreateUserAsync(UserDTO userDTO)
        {
            //first creating a new user
            Users newUser = new();
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            //The users class only has Id, and I need to have the same one for Userdetails to link them
            var latestUser = await GetUserAsync(newUser.UserID);
            //mapping the DTO to UserDetails while making sure it's getting the same Userid as the one made above 
            UserDetails userDetails = new();
            userDetails.UserId = latestUser.UserID;
            userDetails.FirstName = userDTO.FirstName;
            userDetails.LastName = userDTO.LastName;
            userDetails.PassWordHash = userDTO.PassWordHash;
            _context.UserDetails.Add(userDetails);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Returns the user of the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Users> GetUserAsync(int id)
        {
            var user = await _context.Users.Where(u => u.UserID == id).FirstOrDefaultAsync();
            return user;
        }
        /// <summary>
        /// returns the userdetails of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDetails> GetUserDetailsAsync(int id)
        {
            var userDetails = await _context.UserDetails.Where(u => u.UserId == id).FirstOrDefaultAsync();
            return userDetails;
        }
        /// <summary>
        /// Returns the userdetails of every user in the database as an IEnumerable
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserDetails>> GetUserListAsync()
        {
            var UserList = await _context.UserDetails.ToListAsync();
            return UserList;
        }
        /// <summary>
        /// Updates the userdetails of the passed along userDTO, able to update the first name, last name & Password
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            UserDetails userToUpdate = await GetUserDetailsAsync(userDTO.UserId);
            userToUpdate.FirstName = userDTO.FirstName;
            userToUpdate.LastName = userDTO.LastName;
            userToUpdate.PassWordHash = userDTO.PassWordHash;
            await _context.SaveChangesAsync();

        }
        /// <summary>
        /// Deletes the userdetails of the passed along id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteUserAsync(int id)
        {
            var userToDelete =await _context.UserDetails.Where(u => u.UserId == id).FirstOrDefaultAsync();
            if (userToDelete == null)
            {
                throw new Exception("User couldn't be found, are you sure you have the right id?");
            }
            else
            {
                _context.UserDetails.Remove(userToDelete);
            }
        }
    }
}
