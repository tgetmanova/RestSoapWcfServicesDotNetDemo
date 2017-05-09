using System.Collections.Generic;
using System;
using System.Web.Http;

using UserRepositoryServiceApp.Models;
using UserRepositoryServiceApp.Managers;

namespace UserRepositoryService.Controllers
{
    public class TestApiController : ApiController
    {
        /// <summary>
        /// The synchronize profile request manager
        /// </summary>
        private readonly SyncProfileRequestManager syncProfileRequestManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestApiController"/> class.
        /// </summary>
        public TestApiController()
        {
            this.syncProfileRequestManager = new SyncProfileRequestManager();
        }

        public IEnumerable<SyncProfileRequest> Get()
        {
            return this.syncProfileRequestManager.GetSyncProfileRequests();
        }
        
        public SyncProfileRequest Get(Guid Id)
        {
            return this.syncProfileRequestManager.GetSyncProfileRequestById(Id);
        }
        
        public void Post([FromBody]SyncProfileRequest syncProfileRequest)
        {
            this.syncProfileRequestManager.ValidateSyncProfileRequest(syncProfileRequest);
            this.syncProfileRequestManager.CreateSyncProfileRequest(syncProfileRequest);           
        }
        
        public void Put(Guid id, [FromBody]SyncProfileRequest syncProfileRequest)
        {
            this.syncProfileRequestManager.ValidateSyncProfileRequest(syncProfileRequest);
            this.syncProfileRequestManager.UpdateSyncProfileRequest(syncProfileRequest);
        }
        
        public void Delete(Guid id)
        {
            this.syncProfileRequestManager.DeleteSyncProfileRequest(id);
        }
    }
}
