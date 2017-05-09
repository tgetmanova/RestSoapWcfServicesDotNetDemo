using System.Net;
using System;

using Xunit;

using RestSharp;

using UserRepositoryServiceApp.Models;

namespace UserRepositoryServiceTests
{
    /// <summary>
    /// Sync profile requests tests that verify HTTP statuses returned by service.
    /// </summary>
    public class SyncProfileRequestHttpStatusesTests
    {   
        [Theory]
        [Trait("Category", "Negative"), InlineData("RUU")]
        [Trait("Category", "Negative"), InlineData("12")]
        [Trait("Category", "Negative"), InlineData("R3")]
        [Trait("Category", "Negative"), InlineData("R")]
        [Trait("Category", "Negative"), InlineData("R_")]
        [Trait("Category", "Negative"), InlineData("R U")]
        [Trait("Category", "Negative"), InlineData("")]
        [Trait("Category", "Negative"), InlineData(null)]
        public void SyncProfileRequestService_InvalidCountryIsoCode_NewUser_BadRequestShouldBeReturned(string countryIsoCode)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var newRequest = new SyncProfileRequest
            {
                Locale = "FI",
                CountryIsoCode = countryIsoCode,
                UserId = Guid.NewGuid(),
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = false,
                DateModified = DateTime.UtcNow
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(newRequest);

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [Trait("Category", "Negative"), InlineData("12")]
        [Trait("Category", "Negative"), InlineData("L3")]
        [Trait("Category", "Negative"), InlineData("L")]
        [Trait("Category", "Negative"), InlineData("L_")]
        [Trait("Category", "Negative"), InlineData("L T")]
        [Trait("Category", "Negative"), InlineData("ESES")]
        [Trait("Category", "Negative"), InlineData("EN_US")]
        [Trait("Category", "Negative"), InlineData("QQ")]
        [Trait("Category", "Negative"), InlineData("")]
        [Trait("Category", "Negative"), InlineData(null)]
        public void SyncProfileRequestService_InvalidLocale_NewUser_BadRequestShouldBeReturned(string locale)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var userId = Guid.NewGuid();
            var newRequest = new SyncProfileRequest
            {
                Locale = locale,
                CountryIsoCode = "RU",
                UserId = userId,
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = false,
                DateModified = DateTime.UtcNow
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(newRequest);

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
