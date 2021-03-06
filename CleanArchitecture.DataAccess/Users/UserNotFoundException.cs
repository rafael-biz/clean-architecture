using System;

namespace CleanArchitecture.DataAccess.Users
{
    /// <summary>
    /// User not found.
    /// </summary>
    public sealed class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userId) : base($@"User '{ userId }' not found.")
        {

        }
    }
}
