using System.Net;
using System;

using Xunit;

using RestSharp;

using UserRepositoryServiceApp.Models;

namespace UserRepositoryServiceTests
{
    public class SyncProfileRequestLoggingTests
    {  
        //[Theory]
        //[Trait("Category", "Negative"), InlineData("RUU")]
        //[Trait("Category", "Negative"), InlineData("12")]
        //[Trait("Category", "Negative"), InlineData("R3")]
        //[Trait("Category", "Negative"), InlineData("R")]
        //[Trait("Category", "Negative"), InlineData("R_")]
        //[Trait("Category", "Negative"), InlineData("R U")]
        public void SyncProfileRequestService_InvalidCountryIsoCode_NewUser_ErrorShouldBeLogged(string countryIsoCode)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var newRequest = new SyncProfileRequest
            {
                Locale = countryIsoCode,
                CountryIsoCode = "FI",
                UserId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = false,
                DateModified = DateTime.UtcNow
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(newRequest);

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            
        }
        
    }
}
