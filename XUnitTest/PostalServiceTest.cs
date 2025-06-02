using Soup.OrderSystem.Logic;
using Soup.Ordersystem.Objects.Customer;
using System.Threading.Tasks;
using Soup.OrderSystem.Logic.Interfaces;

namespace Soup.OrderSystem.XunitTests;

public class PostalServiceTest
{
    private IPostalCodeServiceAsync _postalCodeService {  get; set; } = new PostalCodeServiceAsync();
    [Fact]
    public async Task Test1()
    {
        _postalCodeService.CreatePostalCode(postalCode: "postalTest", nameOfPlace: "testPlace");
        PostalCode postalCode = await _postalCodeService.GetPostalCodeById("postalTest");
        Assert.NotNull(postalCode);
    }
    [Fact]
    public async Task Test2()
    {
        PostalCode postalCodeId = await _postalCodeService.GetPostalCodeById("PostalTest");
        PostalCode postalCodePlace =await _postalCodeService.GetPostalCodeByPlaceName("testPlace");
        Assert.Equal(postalCodeId.PostalCodeID, postalCodePlace.PostalCodeID);
    }
    [Fact]
    public async Task Test3()
    {
        List<PostalCode> postalList = await _postalCodeService.GetPostalCodes();
        Assert.NotNull(postalList);
    }
    [Fact]
    public async Task Test4()
    {
        IPostalCodeService postalCodeService = new PostalCodeService();
        List<PostalCode> postalList = postalCodeService.GetPostalCodes();
        PostalCode postalCode = postalList.Last();
        await _postalCodeService.DeletePostalCode(postalCode.NameOfPlace);
        PostalCode? postalCodeNull = await _postalCodeService.GetPostalCodeById(postalCode.PostalCodeID);
        Assert.Null(postalCodeNull);
    }
}