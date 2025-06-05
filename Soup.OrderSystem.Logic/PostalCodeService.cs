using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Data;
using Soup.OrderSystem.Logic.Interfaces;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Logic
{
    public class PostalCodeService : IPostalCodeService
    {

        /// <summary>
        /// creates a new postalcode entry with the given place name & postalcode
        /// </summary>
        /// <param name="nameOfPlace"></param>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public void CreatePostalCode(string nameOfPlace, string postalCode)
        {
            try
            {
                using (OrderContext context = new())
                {
                    PostalCode newPostalCode = new();
                    newPostalCode.NameOfPlace = nameOfPlace;
                    newPostalCode.PostalCodeID = postalCode;
                    context.PostalCode.Add(newPostalCode);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while creating the postalcode"+ex.Message);
            }
        }
        /// <summary>
        /// creates a new postalcode entry with the given postalcode
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public void CreatePostalCode(string postalCode)
        {
            try
            {
                using (OrderContext context = new())
                {
                    PostalCode newPostalCode = new();
                    newPostalCode.PostalCodeID = postalCode;
                    context.PostalCode.Add(newPostalCode);
                    context.SaveChanges();
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
        public PostalCode GetPostalCodeByPlaceName(string nameOfPlace)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var postalcode = context.PostalCode.Where(p => p.NameOfPlace == nameOfPlace).FirstOrDefault();
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
        public PostalCode GetPostalCodeById(string postalCodeId)
        {
            try
            {
                using (OrderContext context = new())
                {
                    var postalcode = context.PostalCode.Where(p => p.PostalCodeID == postalCodeId).FirstOrDefault();
                    return postalcode;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the postalcode");
            }
        }
        public List<PostalCode> GetPostalCodes()
        {
            try
            {
                using (OrderContext context = new())
                {
                    List<PostalCode> postalCodes = context.PostalCode.ToList();
                    return postalCodes;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong while retrieving the postalcodes"+ ex.Message);
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
        public void DeletePostalCode(string nameOfPlace)
        {
            try
            {
                var postalCodeToDelete = GetPostalCodeByPlaceName(nameOfPlace);
                using (OrderContext context = new())
                {
                    if (postalCodeToDelete == null)
                    {
                    }
                    else
                    {
                        context.PostalCode.Remove(postalCodeToDelete);
                        context.SaveChanges();
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
