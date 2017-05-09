using System;
using System.Web.Http;
using System.Net.Http;
using System.Net;

using UserRepositoryServiceApp.Models;
using UserRepositoryServiceApp.Managers;
using UserRepositoryServiceApp.Logging;

namespace UserRepositoryService.Controllers
{
    public class SyncProfileRequestController : ApiController
    {
        /// <summary>
        /// The synchronize profile request manager
        /// </summary>
        private readonly SyncProfileRequestManager syncProfileRequestManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncProfileRequestController"/> class.
        /// </summary>
        public SyncProfileRequestController()
        {
            this.syncProfileRequestManager = new SyncProfileRequestManager();
        }
        
        public HttpResponseMessage Post([FromBody]SyncProfileRequest syncProfileRequest)
        {
            var trimmedSyncProfileRequest = this.syncProfileRequestManager.TrimStringsInSyncProfileRequest(syncProfileRequest);

            try
            {
                this.syncProfileRequestManager.ValidateSyncProfileRequest(trimmedSyncProfileRequest);
            }

            catch(ArgumentException exception)
            {
                ServiceLogger.Logger.Error($"User {trimmedSyncProfileRequest.UserId}: {exception.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (this.syncProfileRequestManager.DoesUserExistInRepository(trimmedSyncProfileRequest.UserId))
            {
                this.syncProfileRequestManager.UpdateSyncProfileRequest(trimmedSyncProfileRequest);
                ServiceLogger.Logger.Information($"User {trimmedSyncProfileRequest.UserId} has been modified successfully");
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            else
            {
                this.syncProfileRequestManager.CreateSyncProfileRequest(trimmedSyncProfileRequest);
                ServiceLogger.Logger.Information($"User {trimmedSyncProfileRequest.UserId} has been created successfully");
                return new HttpResponseMessage(HttpStatusCode.Created);
            }          
        }
    }
}
