namespace Soup.OrderSystem.Objects
{
    public enum Status
    {
        New = 0,
        Delivered,
        Canceled
    }
    public class Order
    {
        public int OrderId { get; set; }
        public Customer CustomerId { get; set; }
        //putting the status in an enum for easy access & saving to a db
        
        public Status OrderStatus { get; set; }
    }
}