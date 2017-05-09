using System.Collections.Generic;
using System;

using UserRepositoryServiceApp.Data.Entities;

namespace UserRepositoryServiceApp
{
    /// <summary>
    /// The User Repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <returns>User entities. </returns>
        IEnumerable<UserEntity> GetUsers();

        /// <summary>
        /// Adds the user.
        /// </summary>
        void AddUser(UserEntity user);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        void DeleteUser(Guid userId);


        void UpdateUser(UserEntity userEntity);
    }
}
