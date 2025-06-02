
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ICustomerServiceAsync, CustomerServiceAsync>();
            builder.Services.AddSingleton<IAddressServiceAsync, AddressServiceAsync>();
            builder.Services.AddSingleton<IPostalCodeServiceAsync, PostalCodeServiceAsync>();
            builder.Services.AddSingleton<IOrderServiceAsync, OrderServiceAsync>();
            builder.Services.AddSingleton<IProductServiceAsync, ProductServiceAsync>();
            builder.Services.AddSingleton<IStockActionServiceAsync, StockActionServiceAsync>();
            builder.Services.AddSingleton<IUserServiceAsync, UserServiceAsync>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
