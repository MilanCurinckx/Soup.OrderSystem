using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Logic
{
    public class OrderSystemDI
    {
        public static void Initialize()
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Services.AddSingleton<IAddressService, AddressService>();
            builder.Services.AddSingleton<IOrderService, OrderService>();
            builder.Services.AddSingleton<IPostalCodeService, PostalCodeService>();
            builder.Services.AddSingleton<IProductService,ProductService>();
            builder.Services.AddSingleton<IUserservice,Userservice>();
            builder.Services.AddSingleton<ICustomerService, CustomerService>();
            //builder.Services.AddSingleton<IStockActionService,StockActionService>();

            builder.Services.AddSingleton<ICustomerServiceAsync,CustomerServiceAsync>();
            builder.Services.AddSingleton<IAddressServiceAsync, AddressServiceAsync>();
            builder.Services.AddSingleton<IPostalCodeServiceAsync, PostalCodeServiceAsync>();
            builder.Services.AddSingleton<IOrderServiceAsync, OrderServiceAsync>();
            builder.Services.AddSingleton<IProductServiceAsync, ProductServiceAsync>();
            builder.Services.AddSingleton<IStockActionServiceAsync, StockActionServiceAsync>();
            builder.Services.AddSingleton<IUserServiceAsync, UserServiceAsync>();
            var build = builder.Build();
            
        }
    }
}
