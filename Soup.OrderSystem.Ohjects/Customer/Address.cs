using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Customer
{
    public class Address
    {
        [ForeignKey("Customer_Id")]
        public int Customer_Id { get; set; }
        public string? StreetName { get; set; }
        public int? HouseNumber { get; set; }
        [ForeignKey("Postal_Code")]
        public int Postal_Code { get; set; }
    }
}