using Xunit;

namespace UserRepositoryServiceTests.Utils
{
    /// <summary>
    /// The cleanup test data collection.
    /// </summary>
    [CollectionDefinition("Cleanup Test Data collection")]
    public class CleanupTestDataCollection : ICollectionFixture<CleanupTestDataFixture>
    {
    }
}

