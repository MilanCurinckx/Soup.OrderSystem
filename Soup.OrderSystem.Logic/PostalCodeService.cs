using Microsoft.EntityFrameworkCore;
using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Data;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Logic
{
    public class PostalCodeService() : IPostalCodeService
    {
        private OrderContext _orderContext = new();
        /// <summary>
        /// creates a new postalcode entry with the given place name & postalcode
        /// </summary>
        /// <param name="nameOfPlace"></param>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        public async Task CreatePostalCodeAsync(string nameOfPlace, string postalCode)
        {
            PostalCode newPostalCode = new();
            newPostalCode.NameOfPlace = nameOfPlace;
            newPostalCode.PostalCodeID = postalCode;
            _orderContext.PostalCode.Add(newPostalCode);
            await _orderContext.SaveChangesAsync();
        }
        public async Task CreatePostalCodeAsync(string postalCode)
        {
            PostalCode newPostalCode = new();
            newPostalCode.PostalCodeID = postalCode;
            _orderContext.PostalCode.Add(newPostalCode);
            await _orderContext.SaveChangesAsync();
        }
        /// <summary>
        /// returns the postalcode corresponding to the given place name, if not found it will return null
        /// </summary>
        /// <param name="nameOfPlace"></param>
        /// <returns></returns>
        public async Task<PostalCode> GetPostalCodeAsync(string nameOfPlace)
        {
            var postalcode = await _orderContext.PostalCode.Where(p => p.NameOfPlace == nameOfPlace).FirstOrDefaultAsync();
            return postalcode;
        }
        /// <summary>
        /// returns the postalcode corresponding to the given postalcode (which is used as the Id), if not found it will return null
        /// </summary>
        /// <param name="postalCodeId"></param>
        /// <returns></returns>
        public async Task<PostalCode> GetPostalCodeByIdAsync(string postalCodeId)
        {
            var postalcode = await _orderContext.PostalCode.Where(p => p.PostalCodeID == postalCodeId).FirstOrDefaultAsync();
            return postalcode;
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
        public async Task DeletePostalCodeAsync(string nameOfPlace)
        {
            var postalCodeToDelete = await GetPostalCodeAsync(nameOfPlace);
            if (postalCodeToDelete == null)
            {
            }
            else
            {
                _orderContext.PostalCode.Remove(postalCodeToDelete);
                await _orderContext.SaveChangesAsync();
            }
        }

    }
}
