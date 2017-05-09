using System;
using System.Linq;

namespace UserRepositoryServiceTests.Utils
{
    /// <summary>
    /// Clean up test data fixture
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    internal class CleanupTestDataFixture : IDisposable
    {
        public void Dispose()
        {
            if (TestRunConfiguration.UsersToCleanup.Any())
            {
                TestRunConfiguration.UsersToCleanup.ForEach(UserRepositoryUtils.DeleteSyncProfileRequestByUserId);
            }
        }
    }
}
