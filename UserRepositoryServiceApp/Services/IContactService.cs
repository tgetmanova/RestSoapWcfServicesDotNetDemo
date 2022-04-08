using System;
using System.ServiceModel;

using UserRepositoryServiceApp.Faults;
using UserRepositoryServiceApp.Data.DTO;

namespace UserRepositoryServiceApp.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserManagementService" in both code and config file together.
    [ServiceContract]
    public interface IContactService
    {
        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User Contact Information. </returns>
        [OperationContract]
        [FaultContract(typeof(UserNotFoundFault))]
        ContactInfo GetUserContacts(Guid userId);

        /// <summary>
        /// Updates the user email.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="email">The user email to update.</param>
        [OperationContract]
        void UpdateUserEmail(Guid userId, string email);

        /// <summary>
        /// Updates the user phoneNumber.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="phoneNumber">The user phoneNumber to update.</param>
        [OperationContract]
        void UpdateUserPhoneNumber(Guid userId, string phoneNumber);
    }
}
