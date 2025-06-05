using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.Logic.DTO
{
    public class AddressDTO
    {
        public int AddressID { get; set; }
        public string StreetHouse { get; set; }
        [Range(0, int.MaxValue)]
        public int? BusNumber { get; set; }
        public string PostalCodeId { get; set; }
    }
}
