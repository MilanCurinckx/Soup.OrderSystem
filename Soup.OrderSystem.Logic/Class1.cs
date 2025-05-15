using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Soup.OrderSystem.Logic
{
    internal class Class1
    {
        public static void Initialize()
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Services.AddSingleton<IAddressService, AddressService>();
            builder.Services.AddSingleton<IOrderService, OrderService>();
            builder.Services.AddSingleton<IPostalCodeService, PostalCodeService>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<IUserservice,Userservice>();
            var build = builder.Build();
        }
    }
}
