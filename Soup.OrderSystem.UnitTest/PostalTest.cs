using Soup.Ordersystem.Objects.Customer;
using Soup.OrderSystem.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.UnitTest
{
    [TestClass]
    public class PostalTest
    {
        
        [TestMethod]
        public void Test1()
        {
            PostalCodeService postalCodeService = new PostalCodeService();
            postalCodeService.CreatePostalCode(postalCode: "postalTest", nameOfPlace: "testPlace");
            PostalCode postalCode = postalCodeService.GetPostalCodeById("postalTest");
            Assert.IsNotNull(postalCode);
        }
        //TODO: both of these work but the assert says they're not the same, they are. 
        [TestMethod]
        public void Test2()
        {
            PostalCodeService postalCodeService = new PostalCodeService();
            PostalCode postalCodeId = postalCodeService.GetPostalCodeById("PostalTest");
            PostalCode postalCodePlace = postalCodeService.GetPostalCodeByPlaceName("testPlace");
            Assert.AreEqual(postalCodeId.PostalCodeID, postalCodePlace.PostalCodeID);
        }
        [TestMethod]
        public void Test3()
        {
            PostalCodeService postalCodeService = new PostalCodeService();
            List<PostalCode> postalList = postalCodeService.GetPostalCodes();
            Assert.IsNotNull(postalList);
        }
        [TestMethod]
        public void Test4()
        {
            PostalCodeService postalCodeService = new PostalCodeService();
            List<PostalCode> postalList = postalCodeService.GetPostalCodes();
            PostalCode postalCode = postalList.Last();
            postalCodeService.DeletePostalCode(postalCode.NameOfPlace);
            PostalCode? postalCodeNull = postalCodeService.GetPostalCodeById(postalCode.PostalCodeID);
            Assert.IsNull(postalCodeNull);
        }
    }
}
