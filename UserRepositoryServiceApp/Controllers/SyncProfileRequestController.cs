using System.Collections.Generic;
using System;
using System.Web.Http;

using UserRepositoryServiceApp.Models;
using UserRepositoryServiceApp.Managers;

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
            this.syncProfileRequestManager.CreateSyncProfileRequest(syncProfileRequest);
        }
        
        public void Put(int id, [FromBody]SyncProfileRequest syncProfileRequest)
        {
            this.syncProfileRequestManager.UpdateSyncProfileRequest(syncProfileRequest);
        }
        
        public void Delete(int id)
        {
        }
    }
}
