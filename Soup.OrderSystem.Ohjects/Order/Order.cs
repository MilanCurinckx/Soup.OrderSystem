using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Objects.Order
{
    public class Order
    {
        public Order()
        {
            OrderDetails orderDetails = new OrderDetails();
            OrderId orderId = new OrderId();
            OrderDetails = orderDetails;
            OrderId = orderId;
        }
        public OrderDetails OrderDetails { get; set; }
        public OrderId OrderId { get; set; }
    }
}
