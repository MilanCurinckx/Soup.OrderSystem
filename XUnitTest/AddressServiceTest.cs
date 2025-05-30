using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.XunitTests
{
    internal class AddressServiceTest
    {
        private IAddressService service { get; set; } = new AddressService();
        [Fact]
        public void GetAddressById()
        {
            var result = service.GetAddressById(13);

            Assert.NotNull(result);
        }
        [Fact]
        public void GetAddressesToList()
        {
            var result = service.GetAddressesToList();
            Assert.NotNull(result);
        }
        [Fact]
        public void Test1()
        {
            var totalAddresses = service.GetAddressesToList();
            var amount = totalAddresses.Count();
            Address address = new Address();
            address.PostalCodeId = "0001";
            address.StreetHouse = "testStreet3";
            address.BusNumber = 1;
            service.CreateAddress(address);
            var newTotalAdresses = service.GetAddressesToList();
            var newAmount = newTotalAdresses.Count();
            Assert.NotEqual(newAmount, amount);
        }
        [Fact]
        public void Test2()
        {
            Address addressToUpdate = service.GetAddressById(13);
            addressToUpdate.BusNumber = 2;
            service.UpdateAddress(addressToUpdate);
            Address updatedAddress = service.GetAddressById(13);
            Assert.True(updatedAddress.BusNumber == 2);
            if (updatedAddress.BusNumber == 2)
            {
                addressToUpdate.BusNumber = 1;
                service.UpdateAddress(addressToUpdate);
            }
        }
        [Fact]
        public void Test3()
        {
            var findLatestAddress = service.GetAddressesToList().Max(x => x.AddressID);
            try
            {
                service.DeleteAddress(findLatestAddress);
                var newTotalAddresses = service.GetAddressesToList().Max(x => x.AddressID);
                Assert.False(findLatestAddress == newTotalAddresses);
            }
            catch (Exception ex)
            {
                Assert.Fail("Couldn't remove address because it was still in use by a customer"+ex.Message);
            }
        }
    }
}
