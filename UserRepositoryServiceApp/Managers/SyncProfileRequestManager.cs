using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Globalization;

using UserRepositoryServiceApp.Models;
using UserRepositoryServiceApp.Data.Converters;

namespace UserRepositoryServiceApp.Managers
{
    /// <summary>
    /// Manages sync profile requests.
    /// </summary>
    internal class SyncProfileRequestManager
    {
        /// <summary>
        /// The user repository factory
        /// </summary>
        private readonly UserRepositoryFactory userRepositoryFactory;

        /// <summary>
        /// The user repository
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// The repository type
        /// </summary>
        private readonly string repositoryType = ConfigurationManager.AppSettings["RepositoryType"];

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncProfileRequestManager"/> class.
        /// </summary>
        internal SyncProfileRequestManager()
        {
            this.userRepositoryFactory = new UserRepositoryFactory();
            this.userRepository = this.userRepositoryFactory.GetUserRepository(repositoryType);
        }

        /// <summary>
        /// Gets the synchronize profile requests.
        /// </summary>
        /// <returns>The Sync profile requests. </returns>
        /// 
        internal IEnumerable<SyncProfileRequest> GetSyncProfileRequests()
        {
            return this.userRepository.GetUsers()
                .Select(item => UserSyncRequestConverter.ToSyncProfileRequest(item))
                .ToArray();
        }

        /// <summary>
        /// Gets the synchronize profile request by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns> Sync profile request by Id. </returns>
        /// <exception cref="System.InvalidOperationException">Not found. </exception>
        internal SyncProfileRequest GetSyncProfileRequestById(Guid Id)
        {
            var users = this.userRepository.GetUsers();
            var targetUser = users.FirstOrDefault(i => i.UserId == Id);

            if (targetUser == null)
            {
                throw new InvalidOperationException($"Not found {Id}");
            }

            return UserSyncRequestConverter.ToSyncProfileRequest(targetUser);
        }

        /// <summary>
        /// Validates the already existing user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Whether user already exists. </returns>
        internal bool DoesUserExistInRepository(Guid userId)
        {
            var users = this.userRepository.GetUsers();
            return users.Any(item => item.UserId.Equals(userId));
        }

        /// <summary>
        /// Creates the synchronize profile request.
        /// </summary>
        /// <param name="syncProfileRequest">The synchronize profile request.</param>
        internal SyncProfileRequest CreateSyncProfileRequest(SyncProfileRequest syncProfileRequest)
        {
            var syncProfileRequestToCreate = new SyncProfileRequest
            {
                UserId = syncProfileRequest.UserId,
                RequestId = Guid.NewGuid(),
                Locale = syncProfileRequest.Locale?.Trim(),
                CountryIsoCode = syncProfileRequest.CountryIsoCode?.Trim(),
                AdvertisingOptIn = syncProfileRequest.AdvertisingOptIn,
                DateModified = DateTime.UtcNow
            };

            this.userRepository.AddUser(UserSyncRequestConverter.ToUserEntity(syncProfileRequestToCreate));

            return syncProfileRequestToCreate;
        }

        /// <summary>
        /// Updates the synchronize profile request.
        /// </summary>
        /// <param name="syncProfileRequest">The synchronize profile request.</param>
        internal void UpdateSyncProfileRequest(SyncProfileRequest syncProfileRequest)
        {
            var syncProfileRequestToUpdate = new SyncProfileRequest
            {
                UserId = syncProfileRequest.UserId,
                RequestId = Guid.NewGuid(),
                Locale = syncProfileRequest.Locale?.Trim(),
                CountryIsoCode = syncProfileRequest.CountryIsoCode?.Trim(),
                AdvertisingOptIn = syncProfileRequest.AdvertisingOptIn,
                DateModified = DateTime.UtcNow
            };
            this.userRepository.UpdateUser(UserSyncRequestConverter.ToUserEntity(syncProfileRequestToUpdate));
        }

        /// <summary>
        /// Trims the strings in synchronize profile request.
        /// </summary>
        /// <param name="syncProfileRequest">The synchronize profile request.</param>
        /// <returns>Sync request with trimmed string propeties. </returns>
        internal SyncProfileRequest TrimStringsInSyncProfileRequest(SyncProfileRequest syncProfileRequest)
        {
            return new SyncProfileRequest
            {
                UserId = syncProfileRequest.UserId,
                Locale = syncProfileRequest.Locale?.Trim(),
                CountryIsoCode = syncProfileRequest.CountryIsoCode?.Trim(),
                AdvertisingOptIn = syncProfileRequest.AdvertisingOptIn
            };
        }

        /// <summary>
        /// Validates the new synchronize profile request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.InvalidOperationException">The Sync Profile request is invalid. </exception>
        internal void ValidateSyncProfileRequest(SyncProfileRequest request)
        {
            var validations = new StringBuilder();

            if (!this.IsCountryIsoCodeValid(request.CountryIsoCode))
            {
                validations.Append($"{request.CountryIsoCode} is incorrect ISO code;");
            }

            if (!this.IsLocaleStringValid(request.Locale))
            {
                validations.Append($"{request.Locale} is incorrect locale format;");
            }

            var validationsString = validations.ToString();
            if (!string.IsNullOrEmpty(validationsString))
            {
                throw new ArgumentException($"The Sync Profile request is invalid: {validationsString}");
            }
        }

        internal void DeleteSyncProfileRequest(Guid userId)
        {
            this.userRepository.DeleteUser(userId);
        }

        /// <summary>
        /// Validates the country iso code.
        /// </summary>
        /// <param name="countryIsoCode">The country iso code.</param>
        /// <returns>Whether country ISO is valid. </returns>
        private bool IsCountryIsoCodeValid(string countryIsoCode)
        {
            return countryIsoCode != null 
                && countryIsoCode.All(char.IsLetter)
                && countryIsoCode.Length == 2;
        }      

        /// <summary>
        /// Validates the locale.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <returns>Whether locale is valid. </returns>
        private bool IsLocaleStringValid(string locale)
        {
            if (string.IsNullOrEmpty(locale))
            {
                return false;
            }
            return CultureInfo.GetCultures(CultureTypes.AllCultures).Any(ci => ci.Name.Equals(locale, StringComparison.OrdinalIgnoreCase));
        }
    }
}
