using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : Controller
    {
        private IAddressServiceAsync _addressServiceAsync;
        public AddressController(IAddressServiceAsync addressServiceAsync)
        {
            _addressServiceAsync = addressServiceAsync;
        }
        [HttpPost]
        public async Task CreateAddress(AddressDTO addressDTO)
        {
            var addressCreated = await _addressServiceAsync.CreateAddress(addressDTO);
            
        }
        [HttpGet]

        public async Task<IActionResult> GetId(int addressId)
        {
            var address = await _addressServiceAsync.GetAddressById(addressId);
            return Ok(address);
        }
        [HttpGet]
        [Route("GetLocation")]
        public async Task<IActionResult> GetLocation(string location)
        {
            var address = await _addressServiceAsync.GetAddressByPostalCode(location);
            return Ok(address);
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var address = await _addressServiceAsync.GetAddressesToList();
            return Ok(address);
        }
        [HttpPatch]
        public async Task Update(AddressDTO addressDTO)
        {
           await _addressServiceAsync.UpdateAddress(addressDTO);
        }
        [HttpDelete]
        public async Task Delete(int addressId)
        {
            await _addressServiceAsync.DeleteAddress(addressId);
        }
    }
}
