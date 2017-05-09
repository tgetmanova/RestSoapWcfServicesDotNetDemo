using System;
using System.Collections.Generic;
using System.Linq;

using RestSharp;

using UserRepositoryServiceApp.Models;

namespace UserRepositoryServiceTests.Utils
{
    /// <summary>
    /// User Repository Utils methods that use Test API to manage test data.
    /// </summary>
    internal class UserRepositoryUtils
    {
        /// <summary>
        /// Gets the current synchronize profile requests.
        /// </summary>
        /// <returns>Sync profile requests that are currently presented in repository</returns>
        /// <exception cref="System.Exception"></exception>
        internal static IEnumerable<SyncProfileRequest> GetCurrentSyncProfileRequests()
        {
            var client = TestRunConfiguration.GetTestApiRestClient();

            var request = new RestRequest(Method.GET);

            IRestResponse<List<SyncProfileRequest>> response = client.Execute<List<SyncProfileRequest>>(request);

            if (response.ErrorException != null)
            {
                throw new Exception($"cannot retrieve current repository: {response.ErrorMessage}");
            }

            return response.Data;
        }

        /// <summary>
        /// Gets the synchronize profile request.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Sync Profile request by Id. </returns>
        internal static SyncProfileRequest GetSyncProfileRequestByUserId(Guid userId)
        {
            return GetCurrentSyncProfileRequests().FirstOrDefault(item => item.UserId == userId);
        }

        /// <summary>
        /// Deletes the synchronize profile request by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        internal static void DeleteSyncProfileRequestByUserId(Guid userId)
        {
            var client = TestRunConfiguration.GetTestApiRestClient();

            var request = new RestRequest(Method.GET);

            IRestResponse<List<SyncProfileRequest>> response = client.Execute<List<SyncProfileRequest>>(request);

            var user = response.Data.FirstOrDefault(item => item.UserId == userId);
            if (user != null)
            {
                request = new RestRequest($"/{user.UserId}", Method.DELETE);
                client.Execute(request);
            }
        }

        /// <summary>
        /// Creates the synchronize profile request.
        /// </summary>
        /// <param name="syncProfileRequest">The synchronize profile request.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">cannot create new sync request</exception>
        internal static SyncProfileRequest CreateSyncProfileRequest(SyncProfileRequest syncProfileRequest)
        {
            var client = TestRunConfiguration.GetTestApiRestClient();

            var request = new RestRequest(Method.POST);
            request.AddJsonBody(syncProfileRequest);
            request.RequestFormat = DataFormat.Json;

            IRestResponse<SyncProfileRequest> response = client.Execute<SyncProfileRequest>(request);

            if (response.ErrorException != null || response.Data.UserId == Guid.Empty)
            {
                throw new Exception($"cannot create new sync request: {response.Content}");
            }

            return response.Data;
        }
    }
}
