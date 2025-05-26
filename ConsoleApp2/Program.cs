using Soup.OrderSystem.Logic;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AddressService addressService = new AddressService();
            var result =addressService.GetAddressesToListAsync().Result;
            var counter = 0;
            foreach (var address in result)
            {
                Console.WriteLine(address.StreetHouse);
                counter++;
                Console.WriteLine(counter.ToString());
            }
            Console.ReadKey();
        }
    }
}
