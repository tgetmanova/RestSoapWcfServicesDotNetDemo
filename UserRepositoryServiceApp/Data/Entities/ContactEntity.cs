using System;

namespace UserRepositoryServiceApp.Data.Entities
{
    public class ContactEntity
    {
        public Guid UserId { get; set; }
        public Guid ContactId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
