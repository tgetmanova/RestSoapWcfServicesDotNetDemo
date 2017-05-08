using System;

namespace UserRepositoryServiceApp.Models
{
    /// <summary>
    /// The Account base request.
    /// </summary>
    public class MyAccountRequestBase
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        public Guid RequestId { get; set; }
    }
}