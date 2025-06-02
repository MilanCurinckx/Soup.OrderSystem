using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.Interfaces;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    public class PostalCodeServiceAsync : IPostalCodeServiceAsync
    {
        /// <summary>
        /// creates a new postalcode entry with the given place name & postalcode
        /// </summary>
        /// <param name="nameOfPlace"></param>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public async Task CreatePostalCode(string nameOfPlace, string postalCode)
        {
            try
            {
                using (OrderContext context = new())
                {
                    PostalCode newPostalCode = new();
                    newPostalCode.NameOfPlace = nameOfPlace;
                    newPostalCode.PostalCodeID = postalCode;
                    context.PostalCode.Add(newPostalCode);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the postalcode" + ex.Message);
            }
        }
        /// <summary>
        /// creates a new postalcode entry with the given postalcode
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task CreatePostalCode(string postalCode)
        {
            try
            {
                using (OrderContext context = new())
                {
                    PostalCode newPostalCode = new();
                    newPostalCode.PostalCodeID = postalCode;
                    context.PostalCode.Add(newPostalCode);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the postalcode");
            }
        }
        /// <summary>
        /// returns the postalcode corresponding to the given place name, if not found it will return null
        /// </summary>
        /// <param name="nameOfPlace"></param>
        /// <returns></returns>
        public async Task<PostalCode> GetPostalCodeByPlaceName(string nameOfPlace)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var postalcode = await context.PostalCode.Where(p => p.NameOfPlace == nameOfPlace).FirstOrDefaultAsync();
                    return postalcode;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the postalcode");
            }
        }
        /// <summary>
        /// returns the postalcode corresponding to the given postalcode (which is used as the Id), if not found it will return null
        /// </summary>
        /// <param name="postalCodeId"></param>
        /// <returns></returns>
        public async Task<PostalCode> GetPostalCodeById(string postalCodeId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var postalcode = await context.PostalCode.Where(p => p.PostalCodeID == postalCodeId).FirstOrDefaultAsync();
                    return postalcode;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the postalcode");
            }
        }
        public async Task<List<PostalCode>> GetPostalCodes()
        {
            try
            {
                using (OrderContext context = new())
                {
                    List<PostalCode> postalCodes = await context.PostalCode.ToListAsync();
                    return postalCodes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the postalcodes" + ex.Message);
            }
        }
        /// <summary>
        /// searches for the postalcode corresponding with the given place name, if found it checks if the given place name is different from the one in the DB and updates it. 
        /// </summary>
        /// <param name="postalCodeId"></param>
        /// <param name="nameOfPlace"></param>
        /// <returns></returns>
        //public async Task UpdatePostalCodeAsync(string nameOfPlace)
        //{
        //    var postalCodeToUpdate = await GetPostalCodeAsync(nameOfPlace);
        //    if (postalCodeToUpdate.NameOfPlace != nameOfPlace)
        //    { }
        //    else
        //    {
        //        postalCodeToUpdate.NameOfPlace = nameOfPlace;
        //    }
        //        await _orderContext.SaveChangesAsync();
        //}
        /// <summary>
        /// searches for the postalcode corresponding with the given name of place, if found it will remove the postalcode from the db
        /// </summary>
        /// <param name="nameOfPlace"></param>
        /// <returns></returns>
        public async Task DeletePostalCode(string nameOfPlace)
        {
            try
            {
                var postalCodeToDelete = await GetPostalCodeByPlaceName(nameOfPlace);
                using (OrderContext context = new())
                {
                    if (postalCodeToDelete == null)
                    {
                    }
                    else
                    {
                        context.PostalCode.Remove(postalCodeToDelete);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while deleting the PostalCode");
            }
        }
    }
}
