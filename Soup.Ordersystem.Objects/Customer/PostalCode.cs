using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects.Customer
{
    public class PostalCode
    {
        [Key]
        [Column("PostalCode")]
        public string PostalCodeID { get; set; }
        public string NameOfPlace { get; set; }
    }
}
