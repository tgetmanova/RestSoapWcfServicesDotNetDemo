using System;
using System.ServiceModel;

using UserRepositoryServiceApp.Data.DTO;
using UserRepositoryServiceApp.Models;
using UserRepositoryServiceApp.Faults;
using UserRepositoryServiceApp.Managers;
using UserRepositoryServiceApp.Data.Converters;
using UserRepositoryServiceApp.Logging;

namespace UserRepositoryServiceApp.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserManagementService" in both code and config file together.
    public class UserInfoProviderService : IUserInfoProviderService
    {
        /// <summary>
        /// The synchronize profile request manager
        /// </summary>
        private readonly SyncProfileRequestManager syncProfileRequestManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfoProviderService"/> class.
        /// </summary>
        public UserInfoProviderService()
        {
            this.syncProfileRequestManager = new SyncProfileRequestManager();
        }

        /// <inheritdoc />
        public UserInfo GetUserInfo(Guid userId)
        {
            ServiceLogger.Logger.Information($"Attempt to retrieve User {userId}");
            SyncProfileRequest user = null;

            try
            {
                user = this.syncProfileRequestManager.GetSyncProfileRequestById(userId);
            }
            catch (InvalidOperationException exception)
            {
                var userNotFound = new UserNotFoundFault
                {
                    Reason="Not found is repository",
                    Id = userId
                };

                ServiceLogger.Logger.Error($"User {userId} not found");
                throw new FaultException<UserNotFoundFault>(userNotFound);
            }

            ServiceLogger.Logger.Information($"User {userId} found");
            return UserSyncRequestConverter.ToUserInfo(user);
        }
    }
}
