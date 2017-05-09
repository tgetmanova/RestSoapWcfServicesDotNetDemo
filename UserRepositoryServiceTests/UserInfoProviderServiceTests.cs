using System;

using Xunit;

using UserRepositoryServiceTests.Proxy;

namespace UserRepositoryServiceTests
{
    /// <summary>
    /// The tests against UserInfoProvider WCF service
    /// </summary>
    public class UserInfoProviderServiceTests : IDisposable
    {
        private UserInfoProviderServiceClient providerServiceClient;

        public UserInfoProviderServiceTests()
        {
            this.providerServiceClient = new UserInfoProviderServiceClient();
        }

        public void Dispose()
        {
            this.providerServiceClient.Close();
        }

        [Fact]
        public void UserInfoProviderService_NonExistingUser_ShouldThrow_UserNotFoundFault()
        {
            var randomGuid = Guid.NewGuid();

            var exception = Assert.Throws<System.ServiceModel.FaultException<UserNotFoundFault>>(
                () => this.providerServiceClient.GetUserInfo(randomGuid));

            Assert.Equal("Not found is repository", exception.Detail.Reason);
            Assert.Equal(randomGuid, exception.Detail.Id);
        }
    }
}
