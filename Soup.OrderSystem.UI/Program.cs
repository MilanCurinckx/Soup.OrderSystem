using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Logic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Soup.OrderSystem.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }
            );
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/CustomerLogin";
                    options.AccessDeniedPath = "/Login/Forbidden";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                });
            builder.Services.AddAuthorization(
                options =>
                {
                    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin","true"));
                });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Overview}/{id?}");
            app.Run();
        }
    }
}
