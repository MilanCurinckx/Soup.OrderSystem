using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Soup.OrderSystem.Objects.Customer
{
    public class CustomerId
    {
        [Key]
        public string Customer_Id { get; set; } 
    }

}

