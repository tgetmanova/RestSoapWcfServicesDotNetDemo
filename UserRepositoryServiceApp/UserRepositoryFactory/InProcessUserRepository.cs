using System;
using System.Collections.Generic;
using System.Linq;

using UserRepositoryServiceApp.Data.Entities;

namespace UserRepositoryServiceApp
{
    /// <summary>
    /// This kind of repository stores info in memory.
    /// </summary>
    /// <seealso cref="UserRepositoryServiceApp.UserRepositoryFactory.IUserRepository" />
    internal class InProcessUserRepository : IUserRepository
    {
        /// <summary>
        /// The users
        /// </summary>
        private static IList<UserEntity> users = new List<UserEntity>
            {
                new UserEntity
                {
                    AdvertisingOptIn = true,
                    CountryIsoCode = "RU",
                    DateModified = DateTime.Now.AddDays(-2),
                    RequestId = Guid.NewGuid(),
                    Locale = "RU",
                    UserId = new Guid("8c46ada1-ac9c-4c0e-b1f6-265fa30454c0")
                },
                 new UserEntity
                {
                    AdvertisingOptIn = false,
                    CountryIsoCode = "HU",
                    DateModified = DateTime.Now.AddMonths(-1),
                    RequestId = Guid.NewGuid(),
                    Locale = "HU",
                    UserId = new Guid("14f82c78-390f-4a90-bd99-dbd24d9c968d")
                },
                   new UserEntity
                {
                    AdvertisingOptIn = false,
                    CountryIsoCode = "ES",
                    DateModified = DateTime.Now.AddMinutes(-45),
                    RequestId = Guid.NewGuid(),
                    Locale = "ES-ES",
                    UserId = new Guid("0580769c-e4b0-4f98-8ca9-eb598333ec06")
                }
            };

        /// <inheritdoc />
        public IEnumerable<UserEntity> GetUsers()
        {
            return users;
        }

        /// <inheritdoc />
        public void AddUser(UserEntity user)
        {
            users.Add(user);
        }

        /// <inheritdoc />
        public void DeleteUser(Guid userId)
        {
            var user = users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new InvalidOperationException($"Cannot find user wit ID: {userId}");
            }

            users.Remove(user);
        }

        /// <inheritdoc />
        public void UpdateUser(UserEntity userEntity)
        {
            var user = users.FirstOrDefault(u => u.UserId == userEntity.UserId);
            if (user == null)
            {
                throw new InvalidOperationException($"Cannot find user wit ID: {userEntity.UserId}");
            }

            users[users.IndexOf(user)] = userEntity;
        }
    }
}
