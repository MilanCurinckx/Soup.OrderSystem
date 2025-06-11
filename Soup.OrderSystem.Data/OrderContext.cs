using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Objects.User;



namespace Soup.OrderSystem.Data
{
    public class OrderContext:DbContext
    {
        //these are all the tables for customer
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<PostalCode> PostalCode { get; set; }
        //these are all the tables for users
        public DbSet<Users> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        //these are all the tables for Orders
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> OrderProducts { get; set; }
        public DbSet<StockAction> Stock_Actions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(DatabaseKey.Key);
            //because I keep forgetting what the lazyloading actually does
            //https://www.linkedin.com/pulse/entity-framework-core-lazy-loading-vs-eager-muhammad-mazhar-s0kof/
           
        }
    }
    /*because I'm bound to forget next time I have to use EF, go to the project folder, dotnet ef, check if it's installed, if installed do: dotnet ef migrations add ExampleOfAMigrationName 
     * then do dotnet ef database update --verbose & pray that you did things correctly
     * add
     */
}
