using Microsoft.AspNetCore.Mvc;
using Soup.OrderSystem.Objects.Customer;
using Soup.OrderSystem.Logic.DTO;
using Soup.OrderSystem.Logic.Interfaces;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostalCodeController : Controller
    {
        private readonly IPostalCodeServiceAsync _postalCodeServiceAsync;
        public PostalCodeController(IPostalCodeServiceAsync service) 
        {
            _postalCodeServiceAsync = service;
        }
        [HttpPost]
        public async Task CreatePostalCode(string placeName, string postalCode)
        {
            await _postalCodeServiceAsync.CreatePostalCode(placeName, postalCode);
        }
        [HttpGet]
        public async Task<IActionResult> GetByPlaceName(string placeName)
        {
            var postalCode =await _postalCodeServiceAsync.GetPostalCodeByPlaceName(placeName);
            return Ok(postalCode);
        }
        [HttpGet]
        [Route("{postalCodeId}")]
        public async Task<IActionResult> GetById(string postalCodeId)
        {
            
            var postalCode = await _postalCodeServiceAsync.GetPostalCodeById(postalCodeId);
            return Ok(postalCode);
        }
        [HttpGet("List")]
        public async Task<IActionResult> GetList()
        {
            List<PostalCode> postalCode = new();
            postalCode = await _postalCodeServiceAsync.GetPostalCodes();
            return Ok(postalCode);
        }
        [HttpDelete]
        public async Task Delete(string nameOfPlace)
        {
           await _postalCodeServiceAsync.DeletePostalCode(nameOfPlace);
        }
    }
}
