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
            try
            {
                this.syncProfileRequestManager.ValidateSyncProfileRequest(syncProfileRequest);
            }
            catch(ArgumentException exception)
            {
                ServiceLogger.Logger.Error($"User {syncProfileRequest.UserId}: {exception.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            if (this.syncProfileRequestManager.DoesUserExistInRepository(syncProfileRequest.UserId))
            {
                this.syncProfileRequestManager.UpdateSyncProfileRequest(syncProfileRequest);
                ServiceLogger.Logger.Information($"User {syncProfileRequest.UserId} has been modified successfully");
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            this.syncProfileRequestManager.CreateSyncProfileRequest(syncProfileRequest);
            ServiceLogger.Logger.Information($"User {syncProfileRequest.UserId} has been created successfully");
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
