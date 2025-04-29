using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Customer
{
    [Keyless]
    public class Address
    {
        //[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //int AddressId { get; set; }
        
        public int Customer_Id { get; set; }
        public virtual CustomerId CustomerId { get; set; }
        
        public string? StreetName { get; set; }
        public int? HouseNumber { get; set; }

        public virtual PostalCode PostalCode { get; set; }

    }
}