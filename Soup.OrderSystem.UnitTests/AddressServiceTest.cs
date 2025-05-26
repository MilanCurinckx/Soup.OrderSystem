using Soup.OrderSystem.Logic.Interfaces;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic;

using System.Threading.Tasks;
namespace Soup.OrderSystem.UnitTests
{
    h
    public class AddressServiceTest
    {
        public IAddressService _addressService;
        public AddressServiceTest(AddressService addressService)
        {
            _addressService = addressService;
        }
        []
        public async Task GetAddressesToList()
        {
            var list = await _addressService.GetAddressesToListAsync();
            Assert.NotNull(list);
        }
        [Fact]
        public async Task FindAddressById()
        {
          var result=  await _addressService.GetAddressByIdAsync(3);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task FindAddressByLocation()
        {
            var result = await _addressService.GetAddressByLocationAsync("testStreet");
            Assert.NotNull(result);
        }

        //[Fact]
        //public async Task CreateAddress()
        //{
        //   var AddressDTO = new AddressDTO();
        //    AddressDTO.StreetHouse = "testStreet3";
        //    AddressDTO.BusNumber = 1;
        //    AddressDTO.PostalCodeId = "0001";
        //    await _addressService.CreateAddress(AddressDTO);
            
        //}
    }
    //public class PostalCodeServiceTest
    //{
    //    private IPostalCodeService _postalCodeService;
    //    public PostalCodeServiceTest(IPostalCodeService postalCodeService)
    //    {
    //        _postalCodeService = postalCodeService;
    //    }
    //    [Fact]
    //    public void CreatePostalCode()
    //    {
    //        _postalCodeService.CreatePostalCodeAsync("testplace", "ABCD");
    //        var result = _postalCodeService.GetPostalCodeAsync("testplace");

    //    }
    //}
}