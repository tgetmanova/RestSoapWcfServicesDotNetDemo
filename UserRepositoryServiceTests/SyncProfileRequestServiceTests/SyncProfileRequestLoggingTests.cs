using System;
using System.Linq;

using RestSharp;

using Xunit;

using UserRepositoryServiceApp.Models;
using UserRepositoryServiceTests.Utils;

namespace UserRepositoryServiceTests
{
    /// <summary>
    /// Sync profile requests tests that verify logging logic of service.
    /// </summary>
    [Collection("Cleanup Test Data collection")]
    public class SyncProfileRequestLoggingTests
    {
        [Theory]
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "Logging")]
        [Trait("Category", "Negative"), InlineData("RUU")]
        [Trait("Category", "Negative"), InlineData("12")]
        [Trait("Category", "Negative"), InlineData("R3")]
        [Trait("Category", "Negative"), InlineData("R")]
        public void SyncProfileRequestService_InvalidCountryIsoCode_NewUser_CreateErrorShouldBeLogged(string countryIsoCode)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();
            var userId = Guid.NewGuid();

            var newRequest = new SyncProfileRequest
            {
                Locale = "FI",
                CountryIsoCode = countryIsoCode,
                UserId = userId,
                RequestId = Guid.NewGuid(),
                AdvertisingOptIn = false,
                DateModified = DateTime.UtcNow
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(newRequest);

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            var userRelatedRecords = TestUtils.GetLogEntries().Where(l => l.Contains(userId.ToString())).ToArray();

            Assert.True(userRelatedRecords.Any(), "Cannot find any related entries in the log");

            TestUtils.AggregateAssertions(
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"{newRequest.CountryIsoCode} is incorrect ISO code"))),
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been modified successfully"))),
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been created successfully"))));
        }

        [Theory]
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "Logging")]
        [Trait("Category", "Negative"), InlineData("R_")]
        [Trait("Category", "Negative"), InlineData("R U")]
        [Trait("Category", "Negative"), InlineData("")]
        [Trait("Category", "Negative"), InlineData(null)]
        public void SyncProfileRequestService_InvalidCountryIsoCode_ExistingUser_UpdateErrorShouldBeLogged(string countryIsoCode)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var userId = Guid.NewGuid();
            var expectedRequest = new SyncProfileRequest
            {
                CountryIsoCode = "RU",
                Locale = "RU",
                AdvertisingOptIn = true,
                UserId = userId
            };
            var expectedExistingUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedRequest);

            var updateRequest = new SyncProfileRequest
            {
                Locale = "FI",
                CountryIsoCode = countryIsoCode,
                UserId = userId,
                AdvertisingOptIn = false
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(updateRequest);

            client.Execute(request);

            var userRelatedRecords = TestUtils.GetLogEntries().Where(l => l.Contains(userId.ToString())).ToArray();

            Assert.True(userRelatedRecords.Any(), "Cannot find any related entries in the log");

            TestUtils.AggregateAssertions(
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"{updateRequest.CountryIsoCode} is incorrect ISO code"))),
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been modified successfully"))));

            TestRunConfiguration.UsersToCleanup.Add(userId);
        }

        [Theory]
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "Logging")]
        [Trait("Category", "Negative"), InlineData("12")]
        [Trait("Category", "Negative"), InlineData("L3")]
        [Trait("Category", "Negative"), InlineData("L")]
        [Trait("Category", "Negative"), InlineData("L_")]
        [Trait("Category", "Negative"), InlineData("L T")]
        public void SyncProfileRequestService_InvalidLocale_NewUser_CreateErrorShouldBeLogged(string locale)
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

            client.Execute(request);

            var userRelatedRecords = TestUtils.GetLogEntries().Where(l => l.Contains(userId.ToString())).ToArray();

            Assert.True(userRelatedRecords.Any(), "Cannot find any related entries in the log");

            TestUtils.AggregateAssertions(
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"{newRequest.Locale} is incorrect locale format"))),
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been modified successfully"))),
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been created successfully"))));
        }

        [Theory]
        [Trait("Category", "SyncProfileRequestService")]
        [Trait("Category", "Logging")]
        [Trait("Category", "Negative"), InlineData("ESES")]
        [Trait("Category", "Negative"), InlineData("EN_US")]
        [Trait("Category", "Negative"), InlineData("QQ")]
        [Trait("Category", "Negative"), InlineData("")]
        [Trait("Category", "Negative"), InlineData(null)]
        public void SyncProfileRequestService_InvalidLocale_ExistingUser_UpdateErrorShouldBeLogged(string locale)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var userId = Guid.NewGuid();
            var expectedRequest = new SyncProfileRequest
            {
                CountryIsoCode = "RU",
                Locale = "RU",
                AdvertisingOptIn = true,
                UserId = userId
            };
            var expectedExistingUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedRequest);

            var updateRequest = new SyncProfileRequest
            {
                Locale = locale,
                CountryIsoCode = "FI",
                UserId = userId,
                AdvertisingOptIn = false
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(updateRequest);

            client.Execute(request);

            var userRelatedRecords = TestUtils.GetLogEntries().Where(l => l.Contains(userId.ToString())).ToArray();

            Assert.True(userRelatedRecords.Any(), "Cannot find any related entries in the log");

            TestUtils.AggregateAssertions(
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"{updateRequest.Locale} is incorrect locale format"))),
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been modified successfully"))));

            TestRunConfiguration.UsersToCleanup.Add(userId);
        }

        [Theory]
        [Trait("Category", "SyncProfileRequestService")]
        [InlineData(" RU ", "RU", false)]
        [InlineData("RO", " RO ", true)]
        [InlineData(" KO", "KO ", null)]

        public void SyncProfileRequestService_ValidSynRequest_NonExistingUser_CreatedEventShouldBeLogged(string locale, string countryIsoCode, bool? advertisingOptIn)
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

            var userRelatedRecords = TestUtils.GetLogEntries().Where(l => l.Contains(userId.ToString())).ToArray();

            Assert.True(userRelatedRecords.Any(), "Cannot find any related entries in the log");

            TestUtils.AggregateAssertions(
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"The Sync Profile request is invalid:"))),
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been modified successfully"))),
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been created successfully"))));

            TestRunConfiguration.UsersToCleanup.Add(userId);
        }

        [Theory]
        [Trait("Category", "SyncProfileRequestService")]
        [InlineData(" RU ", "RU", false)]
        [InlineData("RO", " RO ", true)]
        [InlineData(" KO", "KO ", null)]

        public void SyncProfileRequestService_ValidSynRequest_ExistingUser_UpdateEventShouldBeLogged(string locale, string countryIsoCode, bool? advertisingOptIn)
        {
            var client = TestRunConfiguration.GetSyncProfileRequestApiRestClient();

            var userId = Guid.NewGuid();
            var expectedRequest = new SyncProfileRequest
            {
                CountryIsoCode = "US",
                Locale = "EN-US",
                AdvertisingOptIn = false,
                UserId = userId
            };
            var expectedExistingUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedRequest);

            var expectedUpdateRequest = new SyncProfileRequest
            {
                Locale = locale,
                CountryIsoCode = countryIsoCode,
                UserId = userId,
                AdvertisingOptIn = advertisingOptIn
            };

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(expectedUpdateRequest);

            client.Execute(request);

            var userRelatedRecords = TestUtils.GetLogEntries().Where(l => l.Contains(userId.ToString())).ToArray();

            Assert.True(userRelatedRecords.Any(), "Cannot find any related entries in the log");

            TestUtils.AggregateAssertions(
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"The Sync Profile request is invalid:"))),
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} has been modified successfully"))));

            TestRunConfiguration.UsersToCleanup.Add(userId);
        }
    }
}
