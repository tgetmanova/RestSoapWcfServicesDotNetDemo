using System;
using System.Runtime.Serialization;

namespace UserRepositoryServiceApp.Data.DTO
{
    /// <summary>
    /// The User Info.
    /// </summary>
    [DataContract]
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        [DataMember]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the advertising opt in.
        /// </summary>
        [DataMember]
        public bool? AdvertisingOptIn { get; set; }

        /// <summary>
        /// Gets or sets the country iso code.
        /// </summary>
        [DataMember]
        public string CountryIsoCode { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        [DataMember]
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the locale.
        /// </summary>
        [DataMember]
        public string Locale { get; set; }
    }
}
