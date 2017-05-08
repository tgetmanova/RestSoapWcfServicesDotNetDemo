using System;

namespace UserRepositoryServiceApp.Data.Entities
{
    /// <summary>
    /// The User Entity
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Gets or sets the advertising opt in.
        /// </summary>
        public bool AdvertisingOptIn { get; set; }

        /// <summary>
        /// Gets or sets the country iso code.
        /// </summary>
        public string CountryIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        public string Locale { get; set; }
    }
}
