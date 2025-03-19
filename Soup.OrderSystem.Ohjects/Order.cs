namespace Soup.OrderSystem.Ohjects
{
    public class Order
    {
        public int OrderId { get; set; }
        public Customer CustomerId { get; set; }
        //putting the status in an enum for easy access & saving to a db
        public enum Status
        { 
           New,
           Delivered,
           Canceled
        }
    }
}