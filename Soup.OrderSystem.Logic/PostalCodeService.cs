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
        public async Task CreatePostalCodeAsync(string nameOfPlace, string postalCode)
        {
            PostalCode newPostalCode = new();
            newPostalCode.NameOfPlace = nameOfPlace;
            newPostalCode.PostalCodeID = postalCode;
            _orderContext.PostalCode.Add(newPostalCode);
            await _orderContext.SaveChangesAsync();
        }
        public async Task<PostalCode> GetPostalCodeAsync(string nameOfPlace)
        {
            var postalcode = await _orderContext.PostalCode.Where(p => p.NameOfPlace == nameOfPlace).FirstOrDefaultAsync();
            return postalcode;
        }
        public async Task<PostalCode> GetPostalCodeByIdAsync(string postalCodeId)
        {
            var postalcode = await _orderContext.PostalCode.Where(p => p.PostalCodeID == postalCodeId).FirstOrDefaultAsync();
            return postalcode;
        }
        public async Task UpdatePostalCodeAsync(string postalCodeId, string nameOfPlace)
        {
            var postalCodeToUpdate = await GetPostalCodeAsync(nameOfPlace);
            if (postalCodeToUpdate.PostalCodeID != postalCodeId)
            {
                postalCodeToUpdate.PostalCodeID = postalCodeId;
            }
            if (postalCodeToUpdate.NameOfPlace != nameOfPlace)
            {
                postalCodeToUpdate.NameOfPlace = nameOfPlace;
            }
            await _orderContext.SaveChangesAsync();
        }
        public async Task DeletePostalCodeAsync(string nameOfPlace)
        {
            var postalCodeToDelete = await GetPostalCodeAsync(nameOfPlace);
            if (postalCodeToDelete != null)
            {
                _orderContext.PostalCode.Remove(postalCodeToDelete);
                await _orderContext.SaveChangesAsync();
            }
        }

    }
}
