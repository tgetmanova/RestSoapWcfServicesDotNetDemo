using System;

using Xunit;

using RestSharp;

using UserRepositoryServiceApp.Models;

namespace UserRepositoryServiceTests
{
    public class SyncProfileRequestRepositoryTests
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
        public void SyncProfileRequestService_InvalidCountryIsoCode_NewUser_ShouldNotBeAddedToRepository(string countryIsoCode)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var userId = Guid.NewGuid();
            var newRequest = new SyncProfileRequest
            {
                Locale = countryIsoCode,
                CountryIsoCode = "FI",
                UserId = userId,
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = false,
                DateModified = DateTime.UtcNow
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(newRequest);

            client.Execute<SyncProfileRequest>(request);

            var userCreated = UserRepositoryUtils.GetSyncProfileRequestByUserId(userId);
            Assert.Null(userCreated);
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
        public void SyncProfileRequestService_InvalidLocale_NewUser_ShouldNotBeAddedToRepository(string locale)
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

            client.Execute<SyncProfileRequest>(request);

            var userCreated = UserRepositoryUtils.GetSyncProfileRequestByUserId(userId);
            Assert.Null(userCreated);
        }

        [Fact]
        public void SyncProfileRequestService_ValidSynRequest_NonExistingUser_ShouldAddUserToRepository()
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

            client.Execute<SyncProfileRequest>(request);

            var userCreated = UserRepositoryUtils.GetSyncProfileRequestByUserId(userId);

            Assert.NotNull(userCreated);
        }

        [Theory]
        [InlineData(" RU ", "RU", false)]
        [InlineData("RO", " RO ", true)]
        [InlineData(" KO", "KO ", null)]

        public void SyncProfileRequestService_ValidSynRequest_NonExistingUser_ShouldAddUserToRepository_WithCorrectProperties(string locale, string countryIsoCode, bool? advertisingOptIn)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var userId = Guid.NewGuid();
            var expectedSyncRequest = new SyncProfileRequest
            {
                Locale = locale,
                CountryIsoCode = countryIsoCode,
                UserId = userId,
                AdvertisingOptIn = advertisingOptIn
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(expectedSyncRequest);

            client.Execute<SyncProfileRequest>(request);

            var userCreated = UserRepositoryUtils.GetSyncProfileRequestByUserId(userId);
            Assert.NotNull(userCreated);

            TestUtils.AggregateAssertions(
                () => Assert.Equal(expectedSyncRequest.CountryIsoCode.Trim(), userCreated.CountryIsoCode),
                () => Assert.Equal(expectedSyncRequest.Locale.Trim(), userCreated.Locale),
                () => Assert.Equal(expectedSyncRequest.AdvertisingOptIn, userCreated.AdvertisingOptIn),
                () => Assert.Equal(expectedSyncRequest.UserId, userCreated.UserId),
                () => Assert.True(TestUtils.IsDateTimeInTheExpectedRange(
                    userCreated.DateModified.ToUniversalTime(), 
                    DateTime.Now.AddMinutes(-1).ToUniversalTime(), 
                    DateTime.Now.ToUniversalTime())));
        }
    }
}
