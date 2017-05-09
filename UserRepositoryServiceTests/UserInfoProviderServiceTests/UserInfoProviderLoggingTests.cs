using System;
using System.Linq;

using Xunit;

using UserRepositoryServiceTests.Proxy;
using UserRepositoryServiceApp.Models;
using UserRepositoryServiceTests.Utils;

namespace UserRepositoryServiceTests.UserInfoProviderServiceTests
{
    /// <summary>
    /// The tests against UserInfoProvider WCF service that verify user info presented in repository.
    /// </summary>
    public class UserInfoProviderLoggingTests
    {
        /// <summary>
        /// The provider service client
        /// </summary>
        private UserInfoProviderServiceClient providerServiceClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoProviderServiceTests"/> class.
        /// </summary>
        public UserInfoProviderLoggingTests()
        {
            this.providerServiceClient = new UserInfoProviderServiceClient();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.providerServiceClient.Close();
        }

        [Fact]
        [Trait("Category", "UserInfoProviderService")]
        [Trait("Category", "Negative")]
        [Trait("Category", "Logging")]
        public void UserInfoProviderService_NonExistingUser_UserNotFound_ShouldBeLogged()
        {
            var randomGuid = Guid.NewGuid();

            Assert.Throws<System.ServiceModel.FaultException<UserNotFoundFault>>(
                () => this.providerServiceClient.GetUserInfo(randomGuid));

            var userRelatedRecords = TestUtils.GetLogEntries().Where(l => l.Contains(randomGuid.ToString())).ToArray();

            Assert.True(userRelatedRecords.Any(), "Cannot find any related entries in the log");

            TestUtils.AggregateAssertions(
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {randomGuid} not found"))),
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"Attempt to retrieve User {randomGuid}"))),
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {randomGuid} found"))));
        }

        [Fact]
        [Trait("Category", "UserInfoProviderService")]
        [Trait("Category", "Logging")]
        public void UserInfoProviderService_NewlyCreatedUser_UserRetrievedEvent_ShouldBeLogged()
        {
            var userId = Guid.NewGuid();
            var expectedNewRequest = new SyncProfileRequest
            {
                Locale = "IT",
                CountryIsoCode = "IT",
                UserId = userId,
                AdvertisingOptIn = true
            };
            var expectedNewlyCreatedUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedNewRequest);

            var userInfoRetrieved = this.providerServiceClient.GetUserInfo(expectedNewlyCreatedUser.UserId);
            Assert.NotNull(userInfoRetrieved);

            var userRelatedRecords = TestUtils.GetLogEntries().Where(l => l.Contains(userId.ToString())).ToArray();

            Assert.True(userRelatedRecords.Any(), "Cannot find any related entries in the log");

            TestUtils.AggregateAssertions(
                () => Assert.Null(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} not found"))),
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"Attempt to retrieve User {userId}"))),
                () => Assert.NotNull(userRelatedRecords.FirstOrDefault(e => e.Contains($"User {userId} found"))));

            TestRunConfiguration.UsersToCleanup.Add(userId);
        }
    }
}
