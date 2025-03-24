using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Customer
{
    public class CustomerId
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Customer_Id { get; set; } 
    }

}

