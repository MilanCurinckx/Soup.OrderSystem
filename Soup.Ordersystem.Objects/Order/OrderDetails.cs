using Soup.Ordersystem.Objects.Order;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects
{
    public class OrderDetails 
    {
        [ForeignKey(nameof(Customer))]
        public int OrderID { get; set; }    
        [ForeignKey(nameof(Products))]
        public int ProductID { get; set; }
        public int ProductAmount { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }
    }
}
