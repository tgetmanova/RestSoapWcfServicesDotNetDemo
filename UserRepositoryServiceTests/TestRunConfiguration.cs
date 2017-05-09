using System.Configuration;

using RestSharp;

namespace UserRepositoryServiceTests
{
    /// <summary>
    /// Configuration methods for test run.
    /// </summary>
    internal static class TestRunConfiguration
    {
        /// <summary>
        /// The synchronize profile request API URL
        /// </summary>
        private static readonly string SyncProfileRequestApiUrl = ConfigurationManager.AppSettings["SyncProfileRequestApiUrl"];

        /// <summary>
        /// The test API URL
        /// </summary>
        private static readonly string TestApiUrl = ConfigurationManager.AppSettings["TestApiUrl"];

        /// <summary>
        /// Gets the test API rest client.
        /// </summary>
        /// <returns>Test API rest client. </returns>
        internal static RestClient GetTestApiRestClient()
        {
            return new RestClient(TestApiUrl);
        }

        /// <summary>
        /// Gets the synchronize profile request API rest client.
        /// </summary>
        /// <returns>Sync Profile Request API rest client. </returns>
        internal static RestClient GetSyncProfileRequestApiRestClient()
        {
            return new RestClient(SyncProfileRequestApiUrl);
        }
    }
}
