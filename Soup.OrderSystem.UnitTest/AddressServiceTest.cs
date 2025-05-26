using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UnitTest
{
    [TestClass]
    public sealed class AddressServiceTest
    {
        public IAddressService service { get; set; } = new AddressService();


        //[TestMethod]
        //public void GetAddressByLocation()
        //{
        //    var result = service.GetAddressByLocationAsync("testStreet");
        //    Assert.IsNotNull(result);
        //}
        [TestMethod]
        public void GetAddressesToList()
        {
            var result = service.GetAddressesToListAsync();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void CreateAddress()
        {
            var totalAddresses =service.GetAddressesToListAsync();
            var amount= totalAddresses.Result.Count();
            AddressDTO address = new AddressDTO();
            address.PostalCodeId = "0001";
            address.StreetHouse = "testStreet3";
            address.BusNumber = 1;
            service.CreateAddress(address).Wait();
            var newTotalAdresses = service.GetAddressesToListAsync();
            var newAmount = newTotalAdresses.Result.Count();
            amount++;
            Assert.AreEqual(newAmount, amount);
        }
        [TestMethod]
        public void GetAddressById()
        {
            var result = service.GetAddressByIdAsync(13).Result;
            
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void DeleteAddress()
        {
            var findLatestAddress = service.GetAddressesToListAsync().Result.Max(a => a.AddressID);
            service.DeleteAddressAsync(findLatestAddress).Wait();
            var newTotalAddresses = service.GetAddressesToListAsync().Result.Count();
            Assert.IsTrue(findLatestAddress != newTotalAddresses);
        }
        [TestMethod]
        public void UpdateAddress()
        {
            Address addressToUpdate = service.GetAddressByIdAsync(13).Result;
            AddressDTO addressDTO = new AddressDTO();
            addressDTO.PostalCodeId = addressToUpdate.PostalCodeId;
            addressDTO.StreetHouse = addressToUpdate.StreetHouse;
            addressDTO.BusNumber = 2;
            addressDTO.AddressID = addressToUpdate.AddressID;
            service.UpdateAddressAsync(addressDTO).Wait();
            Address updatedAddress = service.GetAddressByIdAsync(13).Result;
            Assert.IsTrue(updatedAddress.BusNumber == 2);
            if (updatedAddress.BusNumber == 2)
            {
                addressDTO.BusNumber = 1;
                service.UpdateAddressAsync(addressDTO).Wait();
            }
            
        }
    }
}
