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
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "HttpStatuses")]
        [Trait("Category", "Negative"), InlineData("RUU")]
        [Trait("Category", "Negative"), InlineData("12")]
        [Trait("Category", "Negative"), InlineData("R3")]
        [Trait("Category", "Negative"), InlineData("R")]
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
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "HttpStatuses")]
        [Trait("Category", "Negative"), InlineData("R_")]
        [Trait("Category", "Negative"), InlineData("R U")]
        [Trait("Category", "Negative"), InlineData("")]
        [Trait("Category", "Negative"), InlineData(null)]
        public void SyncProfileRequestService_InvalidCountryIsoCode_ExistingUser_BadRequestShouldBeReturned(string countryIsoCode)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var expectedUserId = Guid.NewGuid();
            var expectedRequest = new SyncProfileRequest
            {
                CountryIsoCode = "RU",
                Locale = "RU",
                AdvertisingOptIn = true,
                UserId = expectedUserId
            };
            var expectedExistingUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedRequest);

            var updateRequest = new SyncProfileRequest
            {
                Locale = "FI",
                CountryIsoCode = countryIsoCode,
                UserId = expectedUserId,
                AdvertisingOptIn = false
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(updateRequest);

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "HttpStatuses")]
        [Trait("Category", "Negative"), InlineData("12")]
        [Trait("Category", "Negative"), InlineData("L3")]
        [Trait("Category", "Negative"), InlineData("L")]
        [Trait("Category", "Negative"), InlineData("L_")]
        [Trait("Category", "Negative"), InlineData("L T")]
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

        [Theory]
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "HttpStatuses")]
        [Trait("Category", "Negative"), InlineData("ESES")]
        [Trait("Category", "Negative"), InlineData("EN_US")]
        [Trait("Category", "Negative"), InlineData("QQ")]
        [Trait("Category", "Negative"), InlineData("")]
        [Trait("Category", "Negative"), InlineData(null)]
        public void SyncProfileRequestService_InvalidLocale_ExistingUser_BadRequestShouldBeReturned(string locale)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var expectedUserId = Guid.NewGuid();
            var expectedRequest = new SyncProfileRequest
            {
                CountryIsoCode = "RU",
                Locale = "RU",
                AdvertisingOptIn = true,
                UserId = expectedUserId
            };
            var expectedExistingUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedRequest);

            var updateRequest = new SyncProfileRequest
            {
                Locale = locale,
                CountryIsoCode = "FI",
                UserId = expectedUserId,
                AdvertisingOptIn = false
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(updateRequest);

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        [Fact]
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "HttpStatuses")]
        public void SyncProfileRequestService_ValidSynRequest_NonExistingUser_CreatedStatusShouldBeReturned()
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var userId = Guid.NewGuid();
            var newRequest = new SyncProfileRequest
            {
                Locale = "SV",
                CountryIsoCode = "SV",
                UserId = userId,
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = false,
                DateModified = DateTime.UtcNow
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(newRequest);

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "HttpStatuses")]
        public void SyncProfileRequestService_ValidSynRequest_ExistingUser_NoContentStatusShouldBeReturned()
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var expectedUserId = Guid.NewGuid();
            var expectedRequest = new SyncProfileRequest
            {
                CountryIsoCode = "RU",
                Locale = "RU",
                AdvertisingOptIn = false,
                UserId = expectedUserId
            };
            var expectedExistingUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedRequest);

            var expectedUpdateRequest = new SyncProfileRequest
            {
                Locale = "SV",
                CountryIsoCode = "SV",
                UserId = expectedUserId,
                AdvertisingOptIn = true
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(expectedUpdateRequest);

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
