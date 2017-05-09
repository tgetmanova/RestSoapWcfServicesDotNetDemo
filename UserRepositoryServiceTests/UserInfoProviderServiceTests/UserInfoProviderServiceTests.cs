using System;
using System.Linq;

using Xunit;

using UserRepositoryServiceTests.Proxy;
using UserRepositoryServiceApp.Models;

namespace UserRepositoryServiceTests
{
    /// <summary>
    /// The tests against UserInfoProvider WCF service
    /// </summary>
    public class UserInfoProviderServiceTests : IDisposable
    {
        /// <summary>
        /// The provider service client
        /// </summary>
        private UserInfoProviderServiceClient providerServiceClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoProviderServiceTests"/> class.
        /// </summary>
        public UserInfoProviderServiceTests()
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
        public void UserInfoProviderService_ExistingUser_ShouldRetrieveUserInfoByItsGuid()
        {
            var existingUser = UserRepositoryUtils.GetCurrentSyncProfileRequests().FirstOrDefault();
            if (existingUser == null)
            {
                throw new Xunit.Sdk.XunitException("Precondition failed: there are no users to retrieve from repository");
            }

            var userInfoRetrieved = this.providerServiceClient.GetUserInfo(existingUser.UserId);

            TestUtils.AggregateAssertions(
                 () => Assert.Equal(existingUser.CountryIsoCode, userInfoRetrieved.CountryIsoCode),
                 () => Assert.Equal(existingUser.Locale, userInfoRetrieved.Locale),
                 () => Assert.Equal(existingUser.AdvertisingOptIn, userInfoRetrieved.AdvertisingOptIn),
                 () => Assert.Equal(existingUser.UserId, userInfoRetrieved.UserId),
                 () => Assert.Equal(existingUser.DateModified.ToUniversalTime(),userInfoRetrieved.DateModified.ToUniversalTime()));
        }

        [Theory]
        [InlineData("LV", "LV", false)]
        [InlineData("LT", "LT", true)]
        [InlineData("PL", "PL", null)]
        public void UserInfoProviderService_NewlyCreatedUser_ShouldRetrieveUserInfoByItsGuid(string locale, string countryIsoCode, bool? advertisingOptIn)
        {
            var userId = Guid.NewGuid();
            var newRequest = new SyncProfileRequest
            {
                Locale = locale,
                CountryIsoCode = countryIsoCode,
                UserId = userId,
                AdvertisingOptIn = advertisingOptIn
            };
            var newlyCreatedUser = UserRepositoryUtils.CreateSyncProfileRequest(newRequest);

            var userInfoRetrieved = this.providerServiceClient.GetUserInfo(newlyCreatedUser.UserId);

            TestUtils.AggregateAssertions(
                 () => Assert.Equal(newlyCreatedUser.CountryIsoCode, userInfoRetrieved.CountryIsoCode),
                 () => Assert.Equal(newlyCreatedUser.Locale, userInfoRetrieved.Locale),
                 () => Assert.Equal(newlyCreatedUser.AdvertisingOptIn, userInfoRetrieved.AdvertisingOptIn),
                 () => Assert.Equal(newlyCreatedUser.UserId, userInfoRetrieved.UserId),
                 () => Assert.Equal(newlyCreatedUser.DateModified.ToUniversalTime(), userInfoRetrieved.DateModified.ToUniversalTime()));
        }
    }
}
