using System;

namespace UserRepositoryServiceApp.Models
{
    /// <summary>
    /// The Sync Profile request.
    /// </summary>
    /// <seealso cref="MyAccountRequestBase" />
    public class SyncProfileRequest : MyAccountRequestBase
    {
        /// <summary>
        /// Gets or sets the advertising opt in.
        /// </summary>
        public bool? AdvertisingOptIn { get; set; }

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