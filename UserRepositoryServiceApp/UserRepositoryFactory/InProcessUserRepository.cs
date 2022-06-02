using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using UserRepositoryServiceApp.Data.Entities;
using UserRepositoryServiceApp.Util;

namespace UserRepositoryServiceApp
{
    /// <summary>
    /// This kind of repository stores info in memory.
    /// </summary>
    /// <seealso cref="UserRepositoryServiceApp.UserRepositoryFactory.IUserRepository" />
    internal class InProcessUserRepository : IUserRepository
    {
        private static CultureInfo[] ValidLocales = CultureInfo.GetCultures(CultureTypes.AllCultures);

        private static List<UserEntity> users = Enumerable
            .Range(1, RandomUtils.GetRandomInt(1, 25))
            .Select(i => GenerateRandomUserEntity())
            .ToList();

        private static List<ContactEntity> contacts = InitContactsForUsers(users);

        private static List<ContactEntity> InitContactsForUsers (List<UserEntity> users)
        {
            var contacts = new List<ContactEntity>();
            users.ForEach(user =>
            {
                if (RandomUtils.GetRandomBool()) return;
                contacts.Add(GenerateRandomContactEntity(user.UserId));
            });
            return contacts;
        }

        private static UserEntity GenerateRandomUserEntity()
        {
            var randomCultureInfo = RandomUtils.GetRandomElement(ValidLocales);

            return new UserEntity
            {
                AdvertisingOptIn = RandomUtils.GetRandomBool(),
                CountryIsoCode = randomCultureInfo.TwoLetterISOLanguageName,
                DateModified = DateTime.Now.AddDays(RandomUtils.GetRandomInt(-1000, -1)),
                RequestId = Guid.NewGuid(),
                Locale = randomCultureInfo.Name,
                UserId = Guid.NewGuid()
            };
        }

        private static ContactEntity GenerateRandomContactEntity(Guid userId)
        {
            return new ContactEntity
            {
                UserId = userId,
                ContactId = Guid.NewGuid(),
                Email = RandomUtils.GetRandomEmailAddress(),
                PhoneNumber = RandomUtils.GetRandomStringOfNumbers(10)
            };
        }

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

        public List<ContactEntity> GetContacts()
        {
            return contacts;
        }
    }
}
