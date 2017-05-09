using System;
using System.Linq;

using Xunit;

using RestSharp;

using UserRepositoryServiceApp.Models;

namespace UserRepositoryServiceTests
{
    /// <summary>
    /// Sync profile requests tests that verify users' existence in repository and their properties.
    /// </summary>
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
                Locale = "PL",
                CountryIsoCode = countryIsoCode,
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
        [Trait("Category", "Negative"), InlineData("RUU")]
        [Trait("Category", "Negative"), InlineData("12")]
        [Trait("Category", "Negative"), InlineData("R3")]
        [Trait("Category", "Negative"), InlineData("R")]
        [Trait("Category", "Negative"), InlineData("R_")]
        [Trait("Category", "Negative"), InlineData("R U")]
        [Trait("Category", "Negative"), InlineData("")]
        [Trait("Category", "Negative"), InlineData(null)]
        public void SyncProfileRequestService_InvalidCountryIsoCode_ExistingUser_ShouldNotBeUpdatedInRepository(string countryIsoCode)
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

            client.Execute(request);

            var actualUser = UserRepositoryUtils.GetSyncProfileRequestByUserId(expectedUserId);
            Assert.NotNull(actualUser);

            TestUtils.AggregateAssertions(
                () => Assert.Equal(expectedExistingUser.CountryIsoCode, actualUser.CountryIsoCode),
                () => Assert.Equal(expectedExistingUser.Locale, actualUser.Locale),
                () => Assert.Equal(expectedExistingUser.AdvertisingOptIn, actualUser.AdvertisingOptIn),
                () => Assert.Equal(expectedExistingUser.UserId, actualUser.UserId),
                () => Assert.Equal(expectedExistingUser.DateModified, actualUser.DateModified));
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
        public void SyncProfileRequestService_InvalidLocale_ExistingUser_ShouldNotBeUpdatedToRepository(string locale)
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

            client.Execute(request);

            var actualUser = UserRepositoryUtils.GetSyncProfileRequestByUserId(expectedUserId);
            Assert.NotNull(actualUser);

            TestUtils.AggregateAssertions(
                () => Assert.Equal(expectedExistingUser.CountryIsoCode, actualUser.CountryIsoCode),
                () => Assert.Equal(expectedExistingUser.Locale, actualUser.Locale),
                () => Assert.Equal(expectedExistingUser.AdvertisingOptIn, actualUser.AdvertisingOptIn),
                () => Assert.Equal(expectedExistingUser.UserId, actualUser.UserId),
                () => Assert.Equal(expectedExistingUser.DateModified, actualUser.DateModified));
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

        [Fact]
        public void SyncProfileRequestService_ValidSynRequest_ExistingUser_ShouldUpdateUserInRepository()
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

            client.Execute(request);

            var userUpdated = UserRepositoryUtils.GetSyncProfileRequestByUserId(expectedUserId);

            Assert.NotNull(userUpdated);

            TestUtils.AggregateAssertions(
               () => Assert.Equal(expectedUpdateRequest.CountryIsoCode.Trim(), userUpdated.CountryIsoCode),
               () => Assert.Equal(expectedUpdateRequest.Locale.Trim(), userUpdated.Locale),
               () => Assert.Equal(expectedUpdateRequest.AdvertisingOptIn, userUpdated.AdvertisingOptIn),
               () => Assert.Equal(expectedUpdateRequest.UserId, userUpdated.UserId),
               () => Assert.True(TestUtils.IsDateTimeInTheExpectedRange(
                   userUpdated.DateModified.ToUniversalTime(),
                   DateTime.Now.AddMinutes(-1).ToUniversalTime(),
                   DateTime.Now.ToUniversalTime())));
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

        [Theory]
        [InlineData(" RU ", "RU", false)]
        [InlineData("RO", " RO ", true)]
        [InlineData(" KO", "KO ", null)]

        public void SyncProfileRequestService_ValidSynRequest_ExistingUser_ShouldUpdateUserInRepository_WithCorrectProperties(string locale, string countryIsoCode, bool? advertisingOptIn)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var expectedUserId = Guid.NewGuid();
            var expectedRequest = new SyncProfileRequest
            {
                CountryIsoCode = "US",
                Locale = "US",
                AdvertisingOptIn = false,
                UserId = expectedUserId
            };
            var expectedExistingUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedRequest);

            var expectedUpdateRequest = new SyncProfileRequest
            {
                Locale = locale,
                CountryIsoCode = countryIsoCode,
                UserId = expectedUserId,
                AdvertisingOptIn = advertisingOptIn
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(expectedUpdateRequest);

            client.Execute(request);

            var userUpdated = UserRepositoryUtils.GetSyncProfileRequestByUserId(expectedUserId);
            Assert.NotNull(userUpdated);

            TestUtils.AggregateAssertions(
                () => Assert.Equal(expectedUpdateRequest.CountryIsoCode.Trim(), userUpdated.CountryIsoCode),
                () => Assert.Equal(expectedUpdateRequest.Locale.Trim(), userUpdated.Locale),
                () => Assert.Equal(expectedUpdateRequest.AdvertisingOptIn, userUpdated.AdvertisingOptIn),
                () => Assert.Equal(expectedUpdateRequest.UserId, userUpdated.UserId),
                () => Assert.True(TestUtils.IsDateTimeInTheExpectedRange(
                    userUpdated.DateModified.ToUniversalTime(),
                    DateTime.Now.AddMinutes(-1).ToUniversalTime(),
                    DateTime.Now.ToUniversalTime())));
        }
    }
}
