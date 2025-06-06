using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects.User;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    public class UserServiceAsync : IUserServiceAsync
    {

        /// <summary>
        /// Saves a user to the database and creates a UserDetails based on the Id from the new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns> </returns>
        public async Task CreateUser(UserDTO userDTO)
        {
            try
            {
                using (OrderContext _context = new OrderContext())
                {
                    //first creating a new user
                    Users newUser = new();
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    //The users class only has Id, and I need to have the same one for Userdetails to link them
                    var latestUser = await GetUser(newUser.UserID);
                    UserDetails newUserDetails = new();
                    newUserDetails.UserId = latestUser.UserID;
                    newUserDetails.FirstName = userDTO.FirstName;
                    newUserDetails.LastName = userDTO.LastName;
                    newUserDetails.PassWordHash = userDTO.PassWordHash;
                    _context.UserDetails.Add(newUserDetails);
                    _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong trying to create the User " + ex.Message);
            }
        }
        /// <summary>
        /// Returns the user of the given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Users> GetUser(int id)
        {
            try
            {
                using (OrderContext _context = new OrderContext())
                {
                    var user = await _context.Users.Where(u => u.UserID == id).FirstOrDefaultAsync();
                    return user;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong trying to return the User " + ex.Message);
            }

        }
        /// <summary>
        /// returns the userdetails of the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserDetails> GetUserDetails(int id)
        {
            try
            {
                using (OrderContext _context = new OrderContext())
                {
                    var userDetails = await _context.UserDetails.Where(u => u.UserId == id).FirstOrDefaultAsync();
                    return userDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong trying to return the Userdetails " + ex.Message);
            }

        }
        /// <summary>
        /// Returns the userdetails of every user in the database as an IEnumerable
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDetails>> GetUserList()
        {
            try
            {
                using (OrderContext _context = new OrderContext())
                {
                    var UserList = await _context.UserDetails.ToListAsync();
                    return UserList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong trying to return the Userdetails " + ex.Message);
            }

        }
        /// <summary>
        /// Updates the userdetails of the passed along userDTO, able to update the first name, last name & Password
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        public async Task UpdateUser(UserDTO userDTO)
        {
            try
            {
                using (OrderContext _context = new OrderContext())
                {
                    UserDetails userToUpdate = await GetUserDetails(userDTO.UserId);
                    userToUpdate.FirstName = userDTO.FirstName;
                    userToUpdate.LastName = userDTO.LastName;
                    userToUpdate.PassWordHash = userDTO.PassWordHash;
                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Something went wrong trying to update the User " + ex.Message);
            }


        }
        /// <summary>
        /// Deletes the userdetails of the passed along id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteUser(int id)
        {
            try
            {
                using (OrderContext _context = new OrderContext())
                {
                    UserDetails? userToDelete = _context.UserDetails.Where(u => u.UserId == id).FirstOrDefault();
                    if (userToDelete == null)
                    {
                        throw new Exception("User couldn't be found, are you sure you have the right id?");
                    }
                    else
                    {
                        _context.UserDetails.Remove(userToDelete);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong trying to delete the User " + ex.Message);
            }

        }
    }
}
