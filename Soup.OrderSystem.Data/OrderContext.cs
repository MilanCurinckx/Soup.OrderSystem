using Microsoft.EntityFrameworkCore;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Objects.Order;
using Soup.OrderSystem.Objects.Product;
using Soup.OrderSystem.Objects.User;

namespace Soup.OrderSystem.Data
{
    public class OrderContext:DbContext
    {
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<CustomerId> CustomerIds { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PostalCode> PostalCodes { get; set; }
        public DbSet<OrderId> OrderIds { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserId> UserIds { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(DatabaseKey.Key);
            //because I keep forgetting what the lazyloading actually does
            //https://www.linkedin.com/pulse/entity-framework-core-lazy-loading-vs-eager-muhammad-mazhar-s0kof/
           
        }
    }
    /*because I'm bound to forget next time I have to use EF, go to the project folder, dotnet ef, check if it's installed, if installed do: dotnet ef migrations add ExampleOfAMigrationName 
     * then do dotnet ef database update --verbose & pray that you did things correctly
     * add
     
     
     */
}
