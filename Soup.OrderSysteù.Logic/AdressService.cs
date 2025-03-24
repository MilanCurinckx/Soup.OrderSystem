using Soup.OrderSystem.Data;
using Soup.OrderSystem.Objects.Customer;

namespace Soup.OrderSystem.Logic
{
    public class AdressService
    {
        private readonly OrderContext _orderContext;
        private readonly PostalCodeService postalCodeService;
        public AdressService(OrderContext orderContext, PostalCodeService postalCodeService)
        {
            this._orderContext = new OrderContext();
            this.postalCodeService = postalCodeService;
        }

        public void Create(string streetName, int houseNumber, int postalCode)
        {
            Address address = new Address();
            address.StreetName = streetName;
            address.HouseNumber = houseNumber;

        }
    }
    
}
