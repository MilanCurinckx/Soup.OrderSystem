
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Objects.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.XunitTests
{
    public class AddressServiceTest
    {
        private IAddressServiceAsync service { get; set; } = new AddressServiceAsync ();
        [Fact]
        public async void GetAddressById()
        {
            var result = await service.GetAddressById(13);

            Assert.NotNull(result);
        }
        [Fact]
        public async void GetAddressesToList()
        {
            var result =await service.GetAddressesToList();
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Test1()
        {
            var totalAddresses = await service.GetAddressesToList();
            var amount = totalAddresses.Count();
            AddressDTO address = new AddressDTO();
            address.PostalCodeId = "0001";
            address.StreetHouse = "testStreet3";
            address.BusNumber = 1;
            await service.CreateAddress(address);
            var newTotalAdresses = await service.GetAddressesToList();
            var newAmount = newTotalAdresses.Count();
            Assert.NotEqual(amount, newAmount);
        }
        [Fact]
        public async void Test2()
        {
            AddressDTO addressDTO = new AddressDTO();
            Address addressToUpdate = await service.GetAddressById(13);
            addressToUpdate.BusNumber = 2;
            addressDTO.AddressID = addressToUpdate.AddressID;
            addressDTO.StreetHouse= addressToUpdate.StreetHouse;
            addressDTO.BusNumber = addressToUpdate.BusNumber;
            addressDTO.AddressID = addressToUpdate.AddressID;
            await service.UpdateAddress(addressDTO);
            Address updatedAddress = await service.GetAddressById(13);
            Assert.True(updatedAddress.BusNumber == 2);
            if (updatedAddress.BusNumber == 2)
            {
                addressToUpdate.BusNumber = 1;
                await service.UpdateAddress(addressDTO);
            }
        }
        [Fact]
        public async Task Test3()
        {
            List<Address> addressList = await service.GetAddressesToList();
            int addressId= addressList.Max(x => x.AddressID);
            List<Address> newAddressList = new();
            int newAddressTotal = new();
            try
            {
                await service.DeleteAddress(addressId);
                addressList =await service.GetAddressesToList();
                newAddressTotal =addressList.Max(x => x.AddressID);
                Assert.False(addressId == newAddressTotal);
            }
            catch (Exception ex)
            {
                Assert.Fail("Couldn't remove address because it was still in use by a customer"+ex.Message);
            }
        }
    }
}
