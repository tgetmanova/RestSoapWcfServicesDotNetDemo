﻿using System;
using System.ServiceModel;
using System.Collections.Generic;

using UserRepositoryServiceApp.Faults;
using UserRepositoryServiceApp.Data.DTO;
using UserRepositoryServiceApp.Models;

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

        /// <summary>
        /// Gets the user collection.
        /// </summary>
        /// <param name="pagingParams">The pagig parameters.</param>
        /// <returns>Users collection.</returns>
        [OperationContract]
        IList<UserInfo> GetUserInfoList(PagingParams pagingParams);

        /// <summary>
        /// Updates the user information.
        /// </summary>
        /// <param name="user">The user information.</param>
        [OperationContract]
        [FaultContract(typeof(UserNotFoundFault))]
        void UpdateUserInfo(UserInfo user);
    }
}
