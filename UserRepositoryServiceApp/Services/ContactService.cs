using System;
using UserRepositoryServiceApp.Data.DTO;
using UserRepositoryServiceApp.Managers;

namespace UserRepositoryServiceApp.Services
{
    public class ContactService : IContactService
    {
        private readonly SyncProfileRequestManager syncProfileRequestManager;

        public ContactService()
        {
            this.syncProfileRequestManager = new SyncProfileRequestManager();
        }

        public ContactInfo GetUserContacts(Guid userId)
        {
            return syncProfileRequestManager.GetSyncProfileRequestById(userId).Contact;
        }

        public void UpdateUserEmail(Guid userId, string email)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserPhoneNumber(Guid userId, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
