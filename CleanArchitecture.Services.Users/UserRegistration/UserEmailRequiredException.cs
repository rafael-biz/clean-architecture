using System;

namespace CleanArchitecture.Services.Users.UserRegistration
{
    public sealed class UserEmailRequiredException : Exception
    {
        public UserEmailRequiredException(string email) : base($@"The user's email '{ email }' is required.")
        {
        }
    }
}