using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.Objects.Customer
{
    public class PostalCode
    {
        [Key]
        public int Postal_Code { get; set; }
        public string Municipality { get; set; }
    }
}