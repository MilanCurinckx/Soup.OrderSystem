using Soup.OrderSystem.Objects.Customer;

namespace Soup.OrderSystem.Logic.Interfaces
{
    public interface IPostalCodeService
    {
        void CreatePostalCode(string postalCode);
        void CreatePostalCode(string nameOfPlace, string postalCode);
        void DeletePostalCode(string nameOfPlace);
        PostalCode GetPostalCodeByPlaceName(string nameOfPlace);
        PostalCode GetPostalCodeById(string postalCodeId);
        List<PostalCode> GetPostalCodes();
    }
}