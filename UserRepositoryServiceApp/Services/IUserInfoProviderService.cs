using System;
using System.ServiceModel;

using UserRepositoryServiceApp.Faults;
using UserRepositoryServiceApp.Data.DTO;

namespace UserRepositoryServiceApp.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserManagementService" in both code and config file together.
    [ServiceContract]
    public interface IUserInfoProviderService
    {
        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>User Information. </returns>
        [OperationContract]
        [FaultContract(typeof(UserNotFoundFault))]
        UserInfo GetUserInfo(Guid userId);
    }
}
