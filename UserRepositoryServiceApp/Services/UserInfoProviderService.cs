using System;
using System.ServiceModel;
using System.Collections.Generic;
using System.Linq;

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
            var profileRequest = GetSyncProfileRequest(userId);

            return UserSyncRequestConverter.ToUserInfo(profileRequest);
        }

        private SyncProfileRequest GetSyncProfileRequest(Guid userId)
        {
            ServiceLogger.Logger.Information($"Attempt to retrieve User {userId}");
            SyncProfileRequest user = null;

            try
            {
                ServiceLogger.Logger.Information($"Attempt to find user by Id: {userId}");
                var result = this.syncProfileRequestManager.GetSyncProfileRequestById(userId);
                ServiceLogger.Logger.Error($"User {userId} is found");
                return result;
            }
            catch (InvalidOperationException)
            {
                var userNotFound = new UserNotFoundFault
                {
                    Reason = "User is not found is repository",
                    Id = userId
                };

                ServiceLogger.Logger.Error($"User {userId} is not found");
                throw new FaultException<UserNotFoundFault>(userNotFound, userNotFound.Reason);
            }
        }

        public IList<UserInfo> GetUserInfoList(PagingParams pagingParams)
        {
            var resultList = syncProfileRequestManager.GetSyncProfileRequests()
                .Select(u => UserSyncRequestConverter.ToUserInfo(u));

            if (pagingParams == null)
            {
                return resultList.ToList();
            }

            //TODO implement paging logic
            if (pagingParams.OrderBy != null)
            {
            }

            if (pagingParams.FilterBy != null)
            {
            }

            if (pagingParams.PageSize != null)
            {
                resultList = resultList.Take(pagingParams.PageSize.GetValueOrDefault());
            }

            if (pagingParams.PageNumber != null)
            {
                resultList = resultList.Take(pagingParams.PageSize.GetValueOrDefault());
            }

            return resultList.ToList();
        }

        public void UpdateUserInfo(UserInfo updatedUserInfo)
        {
            var profileRequest = GetSyncProfileRequest(updatedUserInfo.UserId);
            profileRequest.CountryIsoCode = updatedUserInfo.CountryIsoCode ?? profileRequest.CountryIsoCode;
            profileRequest.Locale = updatedUserInfo.Locale ?? profileRequest.Locale;
            profileRequest.AdvertisingOptIn = updatedUserInfo.AdvertisingOptIn ?? profileRequest.AdvertisingOptIn;

            syncProfileRequestManager.UpdateSyncProfileRequest(profileRequest);
        }
    }
}
