using Soup.OrderSystem.Objects.Customer;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IPostalCodeServiceAsync
    {
        Task CreatePostalCode(string postalCode);
        Task CreatePostalCode(string nameOfPlace, string postalCode);
        Task DeletePostalCode(string nameOfPlace);
        Task<PostalCode> GetPostalCodeById(string postalCodeId);
        Task<PostalCode> GetPostalCodeByPlaceName(string nameOfPlace);
        Task<List<PostalCode>> GetPostalCodes();
    }
}