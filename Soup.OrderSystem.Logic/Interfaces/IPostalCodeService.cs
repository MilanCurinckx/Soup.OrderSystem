using Soup.Ordersystem.Objects.Customer;

namespace Soup.OrderSystem.Logic
{
    public interface IPostalCodeService
    {
        Task CreatePostalCodeAsync(string nameOfPlace, string postalCode);
        Task DeletePostalCodeAsync(string nameOfPlace);
        Task<PostalCode> GetPostalCodeAsync(string nameOfPlace);
        Task<PostalCode> GetPostalCodeByIdAsync(string postalCodeId);
        Task UpdatePostalCodeAsync(string postalCodeId, string nameOfPlace);
    }
}