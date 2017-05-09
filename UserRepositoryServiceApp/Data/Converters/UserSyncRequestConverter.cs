using System;

using UserRepositoryServiceApp.Data.Entities;
using UserRepositoryServiceApp.Models;
using UserRepositoryServiceApp.Data.DTO;

namespace UserRepositoryServiceApp.Data.Converters
{
    /// <summary>
    /// Converter for data
    /// </summary>
    internal class UserSyncRequestConverter
    {
        /// <summary>
        /// To the user entity.
        /// </summary>
        /// <param name="syncProfileRequest">The synchronize profile request.</param>
        /// <returns> User entity. </returns>
        /// <exception cref="System.ArgumentNullException">syncProfileRequest - syncProfileRequest cannot be null</exception>
        internal static UserEntity ToUserEntity(SyncProfileRequest syncProfileRequest)
        {
            if (syncProfileRequest == null)
            {
                throw new ArgumentNullException(nameof(syncProfileRequest), "syncProfileRequest cannot be null");
            }

            return new UserEntity
            {
                UserId = syncProfileRequest.UserId,
                RequestId = syncProfileRequest.RequestId,
                AdvertisingOptIn = syncProfileRequest.AdvertisingOptIn,
                CountryIsoCode = syncProfileRequest.CountryIsoCode,
                DateModified = syncProfileRequest.DateModified,
                Locale = syncProfileRequest.Locale
            };
        }

        /// <summary>
        /// To the synchronize profile request.
        /// </summary>
        /// <param name="userEntity">The user entity.</param>
        /// <returns>Sync profile request. </returns>
        /// <exception cref="System.ArgumentNullException">userEntity - userEntity cannot be null</exception>
        internal static SyncProfileRequest ToSyncProfileRequest(UserEntity userEntity)
        {
            if (userEntity == null)
            {
                throw new ArgumentNullException(nameof(userEntity), "userEntity cannot be null");
            }

            return new SyncProfileRequest
            {
                UserId = userEntity.UserId,
                RequestId = userEntity.RequestId,
                AdvertisingOptIn = userEntity.AdvertisingOptIn,
                CountryIsoCode = userEntity.CountryIsoCode,
                DateModified = userEntity.DateModified,
                Locale = userEntity.Locale
            };
        }

        /// <summary>
        /// To the user information.
        /// </summary>
        /// <param name="syncProfileRequest">The synchronize profile request.</param>
        /// <returns>User Info.</returns>
        /// <exception cref="System.ArgumentNullException">syncProfileRequest - syncProfileRequest cannot be null</exception>
        internal static UserInfo ToUserInfo(SyncProfileRequest syncProfileRequest)
        {
            if (syncProfileRequest == null)
            {
                throw new ArgumentNullException(nameof(syncProfileRequest), "syncProfileRequest cannot be null");
            }

            return new UserInfo
            {
                UserId = syncProfileRequest.UserId,
                AdvertisingOptIn = syncProfileRequest.AdvertisingOptIn,
                CountryIsoCode = syncProfileRequest.CountryIsoCode,
                DateModified = syncProfileRequest.DateModified,
                Locale = syncProfileRequest.Locale
            };
        }
    }
}
