using System;
using System.Collections.Generic;

using UserRepositoryServiceApp.Data.Entities;

namespace UserRepositoryServiceApp
{
    /// <summary>
    /// This kind of repository stores info in file system.
    /// </summary>
    /// <seealso cref="UserRepositoryServiceApp.UserRepositoryFactory.IUserRepository" />
    internal class FileSystemUserRepository : IUserRepository
    {
        /// <inheritdoc />
        public IEnumerable<UserEntity> GetUsers()
        {
            //// Here we should implement some logic for data retrieving from files of certain type in file system.
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void AddUser(UserEntity user)
        {
            //// Here we should implement some logic for data creation with files of certain type in file system.
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void DeleteUser(Guid userId)
        {
            //// Here we should implement some logic for data removal with files of certain type in file system.
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void UpdateUser(UserEntity userEntity)
        {
            throw new NotImplementedException();
        }
    }
}
