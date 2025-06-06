﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.Order
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey(nameof(Customer))]
        public string CustomerID { get; set; }
        [Required]
        public OrderStatusEnum OrderStatus { get; set; }
        public virtual Customer.Customer Customer { get; set; }
       
    }
    public enum OrderStatusEnum
    {
        New = 1,
        Delivered = 2,
        Canceled = 3,
    }
}
