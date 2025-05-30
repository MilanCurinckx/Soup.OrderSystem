using Soup.OrderSystem.Logic;
using Soup.Ordersystem.Objects.Customer;

namespace Soup.OrderSystem.XunitTests;

public class PostalServiceTest
{
    private IPostalCodeService _postalCodeService {  get; set; } = new PostalCodeService();
    [Fact]
    public void Test1()
    {
        _postalCodeService.CreatePostalCode(postalCode: "postalTest", nameOfPlace: "testPlace");
        PostalCode postalCode = _postalCodeService.GetPostalCodeById("postalTest");
        Assert.NotNull(postalCode);
    }
    [Fact]
    public void Test2()
    {
        PostalCode postalCodeId = _postalCodeService.GetPostalCodeById("PostalTest");
        PostalCode postalCodePlace = _postalCodeService.GetPostalCodeByPlaceName("testPlace");
        Assert.Equal(postalCodeId.PostalCodeID, postalCodePlace.PostalCodeID);
    }
    [Fact]
    public void Test3()
    {
        List<PostalCode> postalList = _postalCodeService.GetPostalCodes();
        Assert.NotNull(postalList);
    }
    [Fact]
    public void Test4()
    {
        IPostalCodeService postalCodeService = new PostalCodeService();
        List<PostalCode> postalList = postalCodeService.GetPostalCodes();
        PostalCode postalCode = postalList.Last();
        _postalCodeService.DeletePostalCode(postalCode.NameOfPlace);
        PostalCode? postalCodeNull = _postalCodeService.GetPostalCodeById(postalCode.PostalCodeID);
        Assert.Null(postalCodeNull);
    }
}