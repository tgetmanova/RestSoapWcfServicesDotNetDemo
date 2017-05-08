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
        /// Creates the synchronize profile request.
        /// </summary>
        /// <param name="syncProfileRequest">The synchronize profile request.</param>
        internal void CreateSyncProfileRequest(SyncProfileRequest syncProfileRequest)
        {
            this.ValidateNewSyncProfileRequest(syncProfileRequest);
            this.userRepository.AddUser(UserSyncRequestConverter.ToUserEntity(syncProfileRequest));
        }

        internal void UpdateSyncProfileRequest(SyncProfileRequest syncProfileRequest)
        {
            
        }

        internal void ValidateExistingSyncProfileRequest(SyncProfileRequest request)
        {
            var validations = new StringBuilder();

            if (this.DoesUserAlreayExist(request.UserId))
            {
                validations.Append($"User {request.UserId} not found");
                validations.AppendLine();
            }

            if (!this.IsCountryIsoCodeValid(request.CountryIsoCode))
            {
                validations.Append($"{request.CountryIsoCode} is incorrect ISO code");
                validations.AppendLine();
            }

            if (!this.IsLocaleStringValid(request.Locale))
            {
                validations.Append($"{request.Locale} is incorrect locale format");
                validations.AppendLine();
            }

            var validationsString = validations.ToString();
            if (!string.IsNullOrEmpty(validationsString))
            {
                throw new InvalidOperationException($"Cannot update sync profile request: {Environment.NewLine} {validationsString}");
            }
        }

        /// <summary>
        /// Validates the new synchronize profile request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.InvalidOperationException">The New Sync Profile request is invalid. </exception>
        internal void ValidateNewSyncProfileRequest(SyncProfileRequest request)
        {
            var validations = new StringBuilder();

            if (!this.DoesUserAlreayExist(request.UserId))
            {
                validations.Append($"User {request.UserId} already added to repository");
                validations.AppendLine();
            }
            if (!this.IsCountryIsoCodeValid(request.CountryIsoCode))
            {
                validations.Append($"{request.CountryIsoCode} is incorrect ISO code");
                validations.AppendLine();
            }

            if (!this.IsLocaleStringValid(request.Locale))
            {
                validations.Append($"{request.Locale} is incorrect locale format");
                validations.AppendLine();
            }

            var validationsString = validations.ToString();
            if (!string.IsNullOrEmpty(validationsString))
            {
                throw new InvalidOperationException($"The New Sync Profile request is invalid: {Environment.NewLine} {validationsString}");
            }
        }

        /// <summary>
        /// Validates the country iso code.
        /// </summary>
        /// <param name="countryIsoCode">The country iso code.</param>
        /// <returns>Whether country ISO is valid. </returns>
        private bool IsCountryIsoCodeValid(string countryIsoCode)
        {
            return countryIsoCode.All(char.IsLetter) && countryIsoCode.Length == 2;
        }

        /// <summary>
        /// Validates the already existing user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Whether user already exists. </returns>
        private bool DoesUserAlreayExist(Guid userId)
        {
            var usersSyncPrfileRequests = this.userRepository.GetUsers();
            return !usersSyncPrfileRequests.Any(item => item.UserId == userId);
        }

        /// <summary>
        /// Validates the locale.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <returns>Whether locale is valid. </returns>
        private bool IsLocaleStringValid(string locale)
        {
            return CultureInfo.GetCultures(CultureTypes.AllCultures).Any(ci => ci.Name == locale);
        }
    }
}
