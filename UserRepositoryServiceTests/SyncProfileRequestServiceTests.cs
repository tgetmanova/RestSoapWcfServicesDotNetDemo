using System.Collections.Generic;

using Xunit;

using RestSharp;

using UserRepositoryServiceApp.Models;

namespace UserRepositoryServiceTests
{
    public class SyncProfileRequestServiceTests
    {
        [Theory]
        [Trait("Category","Negative"), InlineData("RUU")]
        [Trait("Category", "Negative"), InlineData("12")]
        [Trait("Category", "Negative"), InlineData("R3")]
        [Trait("Category", "Negative"), InlineData("R")]
        [Trait("Category", "Negative"), InlineData("R_")]
        [Trait("Category", "Negative"), InlineData("R U")]
        public void SyncProfileRequestService_InvalidCountryIsoCode_NewUser_ShouldNotBeAddedToRepository(string countryIsoCode)
        {
            var client = new RestClient("http://localhost:2828/api/TestApi");

            var request = new RestRequest(Method.GET);

            // execute the request
            IRestResponse response = client.Execute<List<SyncProfileRequest>>(request);
            var content = response.Content;
        }

        [Fact]
        public void SyncProfileRequestService_InvalidCountryIsoCode_NewUser_BadRequestShouldBeReturned()
        {

        }

        [Fact]
        public void SyncProfileRequestService_InvalidCountryIsoCode_NewUser_ErrorShouldBeLogged()
        {

        }
    }
}
