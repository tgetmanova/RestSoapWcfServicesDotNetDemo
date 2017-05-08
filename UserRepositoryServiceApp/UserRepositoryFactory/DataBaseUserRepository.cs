using System;
using System.Collections.Generic;

using UserRepositoryServiceApp.Data.Entities;

namespace UserRepositoryServiceApp
{
    /// <summary>
    /// This kind of repository stores info in some data base.
    /// </summary>
    /// <seealso cref="UserRepositoryServiceApp.UserRepositoryFactory.IUserRepository" />
    class DataBaseUserRepository : IUserRepository
    {
        /// <inheritdoc />
        public IEnumerable<UserEntity> GetUsers()
        {
            //// Here we should implement some logic for data retrieving from data base.
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void AddUser(UserEntity user)
        {
            //// Here we should implement some logic for data creation in the data base.
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void DeleteUser(Guid userId)
        {
            //// Here we should implement some logic for data removal from data base.
            throw new NotImplementedException();
        }
    }
}
