using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Order
{
    
    public class OrderDetails 
    {
        [Key]
        public int OrderdetailsId { get;set;}
        [ForeignKey(nameof(Orders))]
        public int OrderID { get; set; }    
        [ForeignKey(nameof(Products))]
        public int ProductID { get; set; }
        [Required]
        public int ProductAmount { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual Product Products { get; set; }
    }
}
