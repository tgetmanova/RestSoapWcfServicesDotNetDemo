using System;
using System.Runtime.Serialization;

namespace UserRepositoryServiceApp.Faults
{
    /// <summary>
    /// User not found fault contract.
    /// </summary>
    [DataContract]
    [Serializable]
    public class UserNotFoundFault
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        [DataMember]
        public string Reason { get; set; }
    }
}
