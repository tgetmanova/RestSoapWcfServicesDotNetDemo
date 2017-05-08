using System;

namespace UserRepositoryServiceApp
{
    /// <summary>
    /// The Users repository factory
    /// </summary>
    internal class UserRepositoryFactory
    {
        /// <summary>
        /// Gets the user repository.
        /// </summary>
        /// <param name="repositoryType">Type of the repository.</param>
        /// <returns>User repository by type. </returns>
        /// <exception cref="ArgumentException">repositoryType - The repository type parameter provided is invalid</exception>
        internal IUserRepository GetUserRepository(string repositoryType)
        {
            switch (repositoryType)
            {
                case "InProcess":
                    return new InProcessUserRepository();
                case "DataBase":
                    return new DataBaseUserRepository();
                case "FileSystem":
                    return new FileSystemUserRepository();
                default:
                    throw new ArgumentException($"The repository type value {repositoryType} provided is invalid", (nameof(repositoryType)));
            }
        }
    }
}
