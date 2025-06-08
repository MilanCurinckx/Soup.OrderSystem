using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Logic;

namespace Soup.OrderSystem.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<ICustomerServiceAsync, CustomerServiceAsync>();
            builder.Services.AddSingleton<IAddressServiceAsync, AddressServiceAsync>();
            builder.Services.AddSingleton<IPostalCodeServiceAsync, PostalCodeServiceAsync>();
            builder.Services.AddSingleton<IOrderServiceAsync, OrderServiceAsync>();
            builder.Services.AddSingleton<IProductServiceAsync, ProductServiceAsync>();
            builder.Services.AddSingleton<IStockActionServiceAsync, StockActionServiceAsync>();
            builder.Services.AddSingleton<IUserServiceAsync, UserServiceAsync>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}
