using System;
using System.Linq;

using Xunit;

using UserRepositoryServiceTests.Proxy;
using UserRepositoryServiceApp.Models;
using UserRepositoryServiceTests.Utils;

namespace UserRepositoryServiceTests
{
    /// <summary>
    /// The tests against UserInfoProvider WCF service that verify user info presented in repository.
    /// </summary>
    public class UserInfoProviderRepositoryTests : IDisposable
    {
        /// <summary>
        /// The provider service client
        /// </summary>
        private UserInfoProviderServiceClient providerServiceClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoProviderServiceTests"/> class.
        /// </summary>
        public UserInfoProviderRepositoryTests()
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
        public void UserInfoProviderService_NonExistingUser_ShouldThrow_UserNotFoundFault()
        {
            var randomGuid = Guid.NewGuid();

            var exception = Assert.Throws<System.ServiceModel.FaultException<UserNotFoundFault>>(
                () => this.providerServiceClient.GetUserInfo(randomGuid));

            TestUtils.AggregateAssertions(
                () => Assert.Equal("Not found is repository", exception.Detail.Reason),
                () => Assert.Equal(randomGuid, exception.Detail.Id));
        }

        [Fact]
        [Trait("Category", "UserInfoProviderService")]
        public void UserInfoProviderService_ExistingUser_ShouldRetrieveUserInfoByItsGuid()
        {
            var expectedExistingUser = UserRepositoryUtils.GetCurrentSyncProfileRequests().FirstOrDefault();
            if (expectedExistingUser == null)
            {
                throw new Xunit.Sdk.XunitException("Precondition failed: there are no users to retrieve from repository");
            }

            var userInfoRetrieved = this.providerServiceClient.GetUserInfo(expectedExistingUser.UserId);
            Assert.NotNull(userInfoRetrieved);

            TestUtils.AggregateAssertions(
                 () => Assert.Equal(expectedExistingUser.CountryIsoCode, userInfoRetrieved.CountryIsoCode),
                 () => Assert.Equal(expectedExistingUser.Locale, userInfoRetrieved.Locale),
                 () => Assert.Equal(expectedExistingUser.AdvertisingOptIn, userInfoRetrieved.AdvertisingOptIn),
                 () => Assert.Equal(expectedExistingUser.UserId, userInfoRetrieved.UserId),
                 () => Assert.Equal(expectedExistingUser.DateModified.ToUniversalTime(),userInfoRetrieved.DateModified.ToUniversalTime()));
        }

        [Theory]        
        [Trait("Category", "UserInfoProviderService")]
        [InlineData("LV", "LV", false)]
        [InlineData("LT", "LT", true)]
        [InlineData("PL", "PL", null)]
        public void UserInfoProviderService_NewlyCreatedUser_ShouldRetrieveUserInfoByItsGuid(string locale, string countryIsoCode, bool? advertisingOptIn)
        {
            var userId = Guid.NewGuid();
            var expectedNewRequest = new SyncProfileRequest
            {
                Locale = locale,
                CountryIsoCode = countryIsoCode,
                UserId = userId,
                AdvertisingOptIn = advertisingOptIn
            };
            var expectedNewlyCreatedUser = UserRepositoryUtils.CreateSyncProfileRequest(expectedNewRequest);
            Assert.NotNull(expectedNewlyCreatedUser);

            var userInfoRetrieved = this.providerServiceClient.GetUserInfo(expectedNewlyCreatedUser.UserId);
            Assert.NotNull(userInfoRetrieved);

            TestUtils.AggregateAssertions(
                 () => Assert.Equal(expectedNewlyCreatedUser.CountryIsoCode, userInfoRetrieved.CountryIsoCode),
                 () => Assert.Equal(expectedNewlyCreatedUser.Locale, userInfoRetrieved.Locale),
                 () => Assert.Equal(expectedNewlyCreatedUser.AdvertisingOptIn, userInfoRetrieved.AdvertisingOptIn),
                 () => Assert.Equal(expectedNewlyCreatedUser.UserId, userInfoRetrieved.UserId),
                 () => Assert.Equal(expectedNewlyCreatedUser.DateModified.ToUniversalTime(), userInfoRetrieved.DateModified.ToUniversalTime()));

            TestRunConfiguration.UsersToCleanup.Add(userId);
        }
    }
}
